using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository, ISubscriptionRepository subscriptionRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult MyPosts()
        {
            int userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetUserPosts(userId);
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
                int userId = GetCurrentUserProfileId();
                var post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return View("NotAuthorizedDetails");
                }
                else
                {
                    return View(post);
                }
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        // GET: PostController1/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new PostEditViewModel();
            int userId = GetCurrentUserProfileId();
            vm.CategoryOptions = _categoryRepository.GetAll();
            vm.Post = _postRepository.GetUserPostById(id, userId);

            if (vm.Post == null)
            {
                return View("NotAuthorizedDetails");
            }

            return View(vm);
        }

        // POST: PostController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostEditViewModel vm)
        {
            try
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.UserProfileId = GetCurrentUserProfileId();
                _postRepository.UpdatePost(vm.Post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vm);
            }
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        public IActionResult CreateSubscription(int id)
        {
            
            return View("CreateSubscription");
        }

        [HttpPost]
        public IActionResult CreateSubscription(int id, Subscription subscription)
        {

            try
            {
                subscription.BeginDateTime = DateAndTime.Now;
                subscription.SubscriberUserProfileId = GetCurrentUserProfileId();
                subscription.ProviderUserProfileId = _postRepository.GetPublishedPostById(id).UserProfileId;
                subscription.EndDateTime = DateAndTime.Now.AddDays(365);

                _subscriptionRepository.Add(subscription);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private IActionResult RedirectToAction(List<Post> posts)
        {
            throw new NotImplementedException();
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
