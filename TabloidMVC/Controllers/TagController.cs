using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        //private readonly ICategoryRepository _categoryRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            //_categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var posts = _tagRepository.GetAll();
            return View(posts);
        }

        // GET: TagsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //POST: TagsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            tag.Id = id;
            try
            {
                _tagRepository.UpdateTag(tag);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }




        // GET: OwnersController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepository.GetTagById(id);
            return View(tag);
        }

        // POST: OwnersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepository.DeleteTag(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }



    }
}


    // Unused methods

        //public IActionResult Details(int id)
        //{
        //    var post = _postRepository.GetPublishedPostById(id);
        //    if (post == null)
        //    {
        //        int userId = GetCurrentUserProfileId();
        //        post = _postRepository.GetUserPostById(id, userId);
        //        if (post == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return View(post);
        //}

        //public IActionResult Create()
        //{
        //    var vm = new PostCreateViewModel();
        //    vm.CategoryOptions = _categoryRepository.GetAll();
        //    return View(vm);
        //}

        //[HttpPost]
        //public IActionResult Create(PostCreateViewModel vm)
        //{
        //    try
        //    {
        //        vm.Post.CreateDateTime = DateAndTime.Now;
        //        vm.Post.IsApproved = true;
        //        vm.Post.UserProfileId = GetCurrentUserProfileId();

        //        _postRepository.Add(vm.Post);

        //        return RedirectToAction("Details", new { id = vm.Post.Id });
        //    }
        //    catch
        //    {
        //        vm.CategoryOptions = _categoryRepository.GetAll();
        //        return View(vm);
        //    }
        //}

        //private int GetCurrentUserProfileId()
        //{
        //    string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    return int.Parse(id);
    //}
    //}
//}
