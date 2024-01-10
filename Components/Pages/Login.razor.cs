using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace BlazorLearWebApp.Components.Pages;

public partial class Login
{
    
    private string? UserName { get; set; }

    private string? Password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    protected string? Title { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected bool AllowMobile { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    protected bool UseMobileLogin { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected bool AllowOAuth { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    protected bool RememberPassword { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected ElementReference LoginForm { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected string? PostUrl { get; set; }

    // private DotNetObjectReference<AdminLogin>? Interop { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? ReturnUrl { get; set; }

    


    /// <summary>
    /// 
    /// </summary>
    // [Inject]
    // [NotNull]
    // protected ILogin? LoginService { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    protected WebClientService? WebClientService { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IIPLocatorProvider? IPLocatorProvider { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected string? ClassString => CssBuilder.Default("login-wrap")
        .AddClass("is-mobile", UseMobileLogin)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
#if DEBUG
        UserName = "Admin";
        Password = "123789";
#endif
        Title = "系统登录";
        PostUrl = QueryHelper.AddQueryString("Account/Login", new Dictionary<string, string?>
        {
            ["ReturnUrl"] = ReturnUrl,
        });
    }

    /// <summary>
    /// 
    /// </summary>
    protected void OnClickSwitchButton()
    {
        var rem = RememberPassword ? "true" : "false";
        PostUrl = QueryHelper.AddQueryString(UseMobileLogin ? "Account/Mobile" : "Account/Login", new Dictionary<string, string?>()
        {
            [nameof(ReturnUrl)] = ReturnUrl,
            ["remember"] = rem
        });
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remember"></param>
    /// <returns></returns>
    protected Task OnRememberPassword(bool remember)
    {
        OnClickSwitchButton();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    protected void OnSignUp()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    protected void OnForgotPassword()
    {

    }

    

    private void Dispose(bool disposing)
    {
        // if (disposing)
        // {
        //     if (Interop != null)
        //     {
        //         Interop.Dispose();
        //         Interop = null;
        //     }
        // }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}