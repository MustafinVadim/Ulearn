﻿using System.Threading.Tasks;
using System.Web;
using Database.DataContexts;
using Database.Extensions;
using Database.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace uLearn.Web.Controllers
{
	public class AuthenticationManager
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AuthenticationManager));
		private readonly UserManager<ApplicationUser> userManager;
		private readonly UserRolesRepo userRoles;

		private AuthenticationManager()
		{
			var db = new ULearnDb();
			userRoles = new UserRolesRepo(db);
			userManager = new ULearnUserManager(db);
		}

		public static async Task LoginAsync(HttpContextBase context, ApplicationUser user, bool isPersistent)
		{
			log.Info($"Пользователь {user.VisibleName} (логин = {user.UserName}, id = {user.Id}) залогинился");
			await new AuthenticationManager().InternalLoginAsync(context, user, isPersistent);
		}

		public static void Logout(HttpContextBase context)
		{
			GetAuthenticationManager(context).SignOut();
		}

		public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(HttpContextBase context, string xsrfKey, string userId)
		{
			return await GetAuthenticationManager(context).GetExternalLoginInfoAsync(xsrfKey, userId);
		}

		private async Task InternalLoginAsync(HttpContextBase context, ApplicationUser user, bool isPersistent)
		{
			var authenticationManager = GetAuthenticationManager(context);
			authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			var identity = await user.GenerateUserIdentityAsync(userManager, userRoles);
			authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
		}

		public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(HttpContextBase context)
		{
			return await GetAuthenticationManager(context).GetExternalLoginInfoAsync();
		}

		private static IAuthenticationManager GetAuthenticationManager(HttpContextBase context)
		{
			return context.GetOwinContext().Authentication;
		}
	}
}