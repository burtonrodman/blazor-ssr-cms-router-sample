using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCMSRouter.Rendering;

public class PageContentRenderer : ComponentBase 
{
  [Parameter]
  public string pageRoute { get; set; }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.AddMarkupContent(0, $"<p>start page - {pageRoute}</p>");
    builder.AddMarkupContent(0, "<p><a href='/'>Home</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/blah'>blah - returns 404</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/weather'>Weather</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/counter'>Counter</a></p>");
    builder.AddMarkupContent(0, "<p>end page</p>");
  }
}