using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Custom
{
    //public static class ModelStateExtensions
    //{
    //    public static void RemoveFor<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, object>> expression)
    //    {
    //        string expressionText = HtmlHelperExtensions.GetExpressionText(expression);

    //        foreach (var ms in modelState.ToArray())
    //        {
    //            if (ms.Key.StartsWith(expressionText + "."))
    //            {
    //                modelState.Remove(ms.Key);
    //            }
    //        }
    //    }
    //}

    //public static class HtmlHelperExtensions
    //{
    //    public static string GetExpressionText<TModel, TResult>(
    //        this IHtmlHelper<TModel> htmlHelper,
    //        Expression<Func<TModel, TResult>> expression)
    //    {
    //        var expresionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
    //            .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

    //        return expresionProvider.GetExpressionText(expression);
    //    }
    //}
}
