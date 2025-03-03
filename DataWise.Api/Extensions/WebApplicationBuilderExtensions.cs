using Microsoft.AspNetCore.Identity;


namespace DataWise.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureIdentity(
        this WebApplicationBuilder builder,
        IdentityOptions cfg)
    {
        cfg.Password.RequireDigit =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
        cfg.Password.RequireLowercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
        cfg.Password.RequireUppercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
        cfg.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
        cfg.Password.RequiredLength =
            builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
        cfg.Password.RequiredUniqueChars =
            builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

        cfg.SignIn.RequireConfirmedAccount =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
        cfg.SignIn.RequireConfirmedEmail =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
        cfg.SignIn.RequireConfirmedPhoneNumber =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

        cfg.User.RequireUniqueEmail =
            builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
    }
}