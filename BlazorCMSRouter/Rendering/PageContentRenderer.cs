using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorCMSRouter.Rendering;

[Route("/{*pageRoute:nonfile}")]
public class PageContentRenderer : ComponentBase 
{
  [Parameter]
  public string pageRoute { get; set; }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    // TODO - resolve route from database and render the content
    builder.AddMarkupContent(0, $"<p>start page - {pageRoute}</p>");
    builder.AddMarkupContent(0, "<p><a href='/'>Home</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/blah'>blah - WORKS, YAY!!!</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/weather'>Weather</a></p>");
    builder.AddMarkupContent(0, "<p><a href='/counter'>Counter</a></p>");
    builder.AddMarkupContent(0, "<p>end page</p>");
  }
}