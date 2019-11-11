using Inflector;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

public class CustomPluralizer : IPluralizer
{
	private readonly Inflector.Inflector inflector = new Inflector.Inflector(new CultureInfo("en"));

	public string Pluralize(string name)
	{
		return inflector.Pluralize(name) ?? name;
	}
	public string Singularize(string name)
	{
		return inflector.Singularize(name) ?? name;
	}
}

public class CustomDesignTimeServices : IDesignTimeServices
{
	public void ConfigureDesignTimeServices(IServiceCollection services)
	{
		services.AddSingleton<IPluralizer, CustomPluralizer>();
	}
}
