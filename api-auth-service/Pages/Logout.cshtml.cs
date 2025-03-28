﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace api_auth_service.Pages
{
    [IgnoreAntiforgeryToken] 
    public class LogoutModel : PageModel
    {
        [BindProperty]
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the return URL from the query string or default to Login page
            var newUrl = string.IsNullOrEmpty(Request.Query["url"]) ? "/Login" : Request.Query["url"].ToString();
            var uri = Uri.TryCreate(newUrl, UriKind.Absolute, out var newUri);
            var isHasToken = newUri.Query.Contains("token");
            var url = isHasToken ? newUri.GetLeftPart(UriPartial.Path) : newUrl;
            ReturnUrl = url;

            // Ensure we clear the authentication cookie
            if (Request.Cookies.ContainsKey("googleToken"))
            {
                Response.Cookies.Delete("googleToken");
            }
            // Complete the async method properly
            await Task.CompletedTask;

            // Redirect to the specified return URL or default to Login page if none exists
            //var redirectUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? "/Login" : ReturnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newUrl = string.IsNullOrEmpty(Request.Query["url"]) ? "/Login" : Request.Query["url"].ToString();
            var uri = Uri.TryCreate(newUrl, UriKind.Absolute, out var newUri);
            var isHasToken = newUri.Query.Contains("token");
            var url = isHasToken ? newUri.GetLeftPart(UriPartial.Path) : newUrl;
            ReturnUrl = url;

            // Ensure we clear the authentication cookie
            Response.Cookies.Delete("googleToken");
            // Complete the async method properly
            await Task.CompletedTask;

            // Redirect to the specified return URL or default to Login page if none exists
            var redirectUrl = string.IsNullOrWhiteSpace(ReturnUrl) ? "/Login" : ReturnUrl;

            return Redirect(redirectUrl);
        }
    }
}
