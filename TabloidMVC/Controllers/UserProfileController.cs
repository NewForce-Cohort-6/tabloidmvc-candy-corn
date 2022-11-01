using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers

{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository, IUserTypeRepository userTypeRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userTypeRepository = userTypeRepository;
        }

        // GET: UserProfileController
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetAll();
            List<UserType> userTypes = _userTypeRepository.GetUserTypes();

            UserProfileViewModel vm = new()
            {
                UserProfiles = userProfiles,
                UserTypes = userTypes,
            };

            return View(vm);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            UserProfile user = _userProfileRepository.GetById(id);
            List<UserType> userTypes = _userTypeRepository.GetUserTypes();

            UserProfileViewModel vm = new()
            {
                UserProfile = user,
                UserTypes = userTypes
            };

            return View(vm);
        }

        // GET: UserProfileController/Register
        public ActionResult Register()
        {
            List<UserType> userTypes = _userTypeRepository.GetUserTypes();

            UserProfileViewModel vm = new()
            {
                UserProfile = new UserProfile(),
                UserTypes = userTypes
            };

            return View(vm);
        }

        // POST: UserProfileController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserProfileViewModel vm)
        {
            try
            {
                vm.UserProfile.CreateDateTime = DateTime.Now;

                _userProfileRepository.AddUser(vm.UserProfile);

                return RedirectToAction("Login", "Account");
            }
            catch
            {
                return View(vm.UserProfile);
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
