// https://chrissainty.com/building-a-custom-router-for-blazor/#:~:text=Building%20a%20Custom%20Router%20for%20Blazor%201%20The,creating%20the%20new%20router%20component%2C%20named%20ConventionRouter.%20

using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorCMSRouter.Routing;

public class ContentRouter : Microsoft.AspNetCore.Components.IComponent, IHandleAfterRender, IDisposable
{
  RenderHandle _renderHandle;
  bool _navigationInterceptionEnabled;
  string _location;

  [Inject] private NavigationManager NavigationManager { get; set; }
  [Inject] private INavigationInterception NavigationInterception { get; set; }
  [Inject] RouteManager RouteManager { get; set; }

  [Parameter] public RenderFragment NotFound { get; set; }
  [Parameter] public RenderFragment<Microsoft.AspNetCore.Components.RouteData> Found { get; set; }
  [Parameter] public Assembly AppAssembly { get; set; }

  public void Attach(RenderHandle renderHandle)
  {
    _renderHandle = renderHandle;
    _location = NavigationManager.Uri;
    NavigationManager.LocationChanged += HandleLocationChanged;
  }

  public Task SetParametersAsync(ParameterView parameters)
  {
    parameters.SetParameterProperties(this);

    if (Found == null)
    {
      throw new InvalidOperationException($"The {nameof(ContentRouter)} component requires a value for the parameter {nameof(Found)}.");
    }

    if (NotFound == null)
    {
      throw new InvalidOperationException($"The {nameof(ContentRouter)} component requires a value for the parameter {nameof(NotFound)}.");
    }

    RouteManager.Initialize();
    Refresh();

    return Task.CompletedTask;
  }

  public Task OnAfterRenderAsync()
  {
    if (!_navigationInterceptionEnabled)
    {
      _navigationInterceptionEnabled = true;
      return NavigationInterception.EnableNavigationInterceptionAsync();
    }

    return Task.CompletedTask;
  }

  public void Dispose()
  {
    NavigationManager.LocationChanged -= HandleLocationChanged;
  }

  private void HandleLocationChanged(object sender, LocationChangedEventArgs args)
  {
    _location = args.Location;
    Refresh();
  }

  private void Refresh()
  {
    var relativeUri = NavigationManager.ToBaseRelativePath(_location);
    var parameters = ParseQueryString(relativeUri);

    if (relativeUri.IndexOf('?') > -1)
    {
      relativeUri = relativeUri.Substring(0, relativeUri.IndexOf('?'));
    }

    // var segments = relativeUri.Trim().Split('/', StringSplitOptions.RemoveEmptyEntries);
    var matchResult = RouteManager.Match(relativeUri);

    if (matchResult.IsMatch)
    {
      parameters["pageRoute"] = relativeUri;
      var routeData = new Microsoft.AspNetCore.Components.RouteData(
          matchResult.MatchedRoute.Handler,
          parameters);

      _renderHandle.Render(Found(routeData));
    }
    else
    {
      _renderHandle.Render(NotFound);
    }
  }

  private Dictionary<string, object> ParseQueryString(string uri)
  {
    var querystring = new Dictionary<string, object>();

    foreach (string kvp in uri.Substring(uri.IndexOf("?") + 1).Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
    {
      if (kvp != "" && kvp.Contains("="))
      {
        var pair = kvp.Split('=');
        querystring.Add(pair[0], pair[1]);
      }
    }

    return querystring;
  }
}