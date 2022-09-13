using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.WebUI.Extensions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken] //GET haricindeki tüm methodlar validate edilmek zorunda
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender,ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService= cartService;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // generate token

                var tokenOriginal = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = tokenOriginal
                });
                // send email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınız için e-mail onayı gerekiyor.", $"<a href='http://localhost:5000{callbackUrl}'>Hesabımı Onayla<a/>");
                
                TempData.Put("message",new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message ="Onaylamak için gerekli yönergeler e-posta adresinize gönderildi." ,
                    Css="info"
                });
                return RedirectToAction("Login", "Account");
            }


            ModelState.AddModelError("", "Bilinmeyen hata oluştu lütfen tekrar deneyiniz.");
            return View(model);
        }


        

        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-mail ile bir hesap bulunmuyor.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Hesabınız e-mail onayı bekliyor.");

                var tokenOriginal = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = tokenOriginal
                });
                // send email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınız için e-mail onayı gerekiyor.", $"<a href='http://localhost:5000{callbackUrl}'>Hesabımı Onayla<a/>");

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Onaylamak için gerekli yönergeler e-posta adresinize gönderildi.",
                    Css = "info"
                });
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Şifre yanlış!");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessage()
            {
                Title = "",
                Message = "Oturum başarıyla kapatıldı.",
                Css = "warning"
            });
            return Redirect("~/");
        }



        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            

            if (userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Geçersiz Token",
                    Message = "",
                    Css = "danger"
                });
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "HATA",
                    Message = "Böyle bir üyemiz bulunmuyor.",
                    Css = "danger"
                });
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                //create cart object
                _cartService.InitializeCart(user.Id);

                TempData.Put("message", new ResultMessage()
                {
                    Title = "E-mail onaylandı!",
                    Message = "",
                    Css = "success"
                });
                return RedirectToAction("Login");
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "E-mail onaylanamadı!",
                Message = "Teknik bir sorun olabilir. Daha sonra tekrar deneyebilirsiniz.",
                Css = "danger"
            });
            return View();


        }

        public IActionResult ResetPassword(string userId, string token)
        {
           

            if (token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "TOKEN ERROR",
                    Message = "Şifre Yenilenmedi. Teknik bir sorun olabilir. Daha sonra tekrar deneyebilirsiniz.",
                    Css = "danger"
                });
                return RedirectToAction("Home", "Index");
            }

            var model = new ResetPasswordModel
            {
                Token = token,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            

            if (!ModelState.IsValid)
            {
                return View(model);
                
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "",
                    Message = "Böyle bir üyemiz bulunmuyor.",
                    Css = "danger"
                });

            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Şifreniz değiştirildi!",
                    Message = "",
                    Css = "success"
                });


                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            

            if (string.IsNullOrEmpty(email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Uyarı",
                    Message = "Bir e-mail girdiğinizden emin olun.",
                    Css = "danger"
                });
                
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "",
                    Message = "Böyle bir üyemiz bulunmuyor.",
                    Css = "danger"
                });
                return View();
            }

            var tokenOriginal = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {

                token = tokenOriginal
            });
            // send email
            await _emailSender.SendEmailAsync(email, "Şifre Yenileme Talebiniz", $"<a href='http://localhost:5000{callbackUrl}'>Şifreyi Yenile<a/>");
            ModelState.AddModelError("", "Şifre yenileme linki gönderildi.");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}