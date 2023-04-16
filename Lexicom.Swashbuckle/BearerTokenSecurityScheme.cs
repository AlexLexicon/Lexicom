using Microsoft.OpenApi.Models;

namespace Lexicom.Swashbuckle;
public class BearerTokenSecurityScheme : OpenApiSecurityScheme
{
    public BearerTokenSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        };
        Description = "JWT Authorization header using the Bearer scheme.";
        Name = "Authorization";
        In = ParameterLocation.Header;
        Type = SecuritySchemeType.Http;
        Scheme = "bearer";
        BearerFormat = "JWT";
    }
}
