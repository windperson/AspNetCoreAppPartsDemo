using System;
using System.Linq;
using ListFeatureLib.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ListFeatureLib
{
    public class ListFeaturesController : Controller
    {
        private readonly ApplicationPartManager _partManager;

        public ListFeaturesController(ApplicationPartManager partManager)
        {
            _partManager = partManager;
        }

        public IActionResult Index()
        {
            var viewModel = new FeaturesViewModel();

            var controllerFeature = GetFeature<ControllerFeature>(_partManager);
            var tagHelperFeature = GetFeature<TagHelperFeature>(_partManager);
            var viewComponentFeature = GetFeature<ViewComponentFeature>(_partManager);

            viewModel.Controllers = controllerFeature.Controllers.ToList();
            viewModel.TagHelpers = tagHelperFeature.TagHelpers.ToList();
            viewModel.ViewComponents = viewComponentFeature.ViewComponents.ToList();

            return View(viewModel);
        }

        public T GetFeature<T>(ApplicationPartManager partManager) where T : new()
        {
            var ret = new T();
            partManager.PopulateFeature<T>(ret);
            return ret;
        }
    }
}
