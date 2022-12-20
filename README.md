ProfileSharp was built to allow developers to quickly test on their local machines and automate unit tests without manually generating a large amount of mock data. ProfileSharp provides an easy way to collect mock data and enable its use within your domain.

>**NOTE:** This is an early proof of concept and could change as it matures.

#### Planned Features

* Automatic Unit Test Generation and Execution at Runtime within your unit tests based on profiled data.


#### Drawbacks

* Mock data can only be collected at runtime.
* If the signature of a mocked method changes it will no longer be compatible with preivously profiled mocking data.
* Random data is not supported in unit tests. (This may be resolved with an Attribute or alternative means in the future)
* Mocking is currently only supported within an AspNet runtime.
* Only methods and properties can profiled/mocked.

# How?

Install the `ProfileSharp.AspNetCore` package from nuget.

In your `Startup.cs` call the following methods.


```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddProfileSharp(o =>
	{
		o.AddProfiling(o =>
		{
			o.UseFileStore(@"D:\#temp\ProfilingStore");
		});

		o.AddMocking(o =>
		{
			o.UseFileStore(@"D:\#temp\ProfilingStore");
		});
	});
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	app.UseProfileSharp(config =>
	{
		if(env.IsDevelopment())
		{
			// Enable Mocking Mode, when this mode is set ProfileSharp will mock the
			// specified parts of your domain.
			config.EnableMocking();
		}
		else if(env.IsStaging())
		{
			// Enable Profiling Mode, when this mode is set ProfileSharp will profile the
			// specified parts of your domain.
			config.EnableProfiling();
		}
		else
		{
			// Disable ProfileSharp, Mocking and Profiling will not occur.
			config.Disable();
		}
	
	});
}

```

## If your project is using NServiceBus

Install the `ProfileSharp.NServiceBus` package from nuget.

When configuration your `EndpointConfiguration` call the following methods

```csharp
endpointConfiguration.UseProfileSharp();
```

## ProfileSharp Attributes

For ProfileSharp to work, you must specify what parts of your domain will be intercepted. This is done using the `EnableProfileSharpAttribute`.

```csharp
[EnableProfileSharp]
public interface IStudentRepository
{
	Task CreateAsync(StudentModel student);

	Task<StudentModel> GetAsync(Guid studentId);

	Task DeleteAsync(Guid studentId);
}
```

You can also only enable ProfileSharp for specific methods.

```csharp
public interface IStudentRepository
{
	// This method will not intercepted.
	Task CreateAsync(StudentModel student);

	[EnableProfileSharp]
	Task<StudentModel> GetAsync(Guid studentId);

	// This method will not intercepted.
	Task DeleteAsync(Guid studentId);
}
```

Or disable ProfileSharp for specific methods.

```csharp
[EnableProfileSharp]
public interface IStudentRepository
{
	Task CreateAsync(StudentModel student);
	
	Task<StudentModel> GetAsync(Guid studentId);

	// This method will not intercepted.
	[DisableProfileSharp]
	Task DeleteAsync(Guid studentId);
}
```

## Releases

|Package|Downloads|NuGet|
|-|-|-|
|**ProfileSharp**|![](https://img.shields.io/nuget/dt/ProfileSharp?style=for-the-badge)|[![Nuget](https://img.shields.io/nuget/v/ProfileSharp.svg?style=for-the-badge)](https://www.nuget.org/packages/ProfileSharp/)|
|**ProfileSharp.AspNetCore**|![](https://img.shields.io/nuget/dt/ProfileSharp.AspNetCore?style=for-the-badge)|[![Nuget](https://img.shields.io/nuget/v/ProfileSharp.AspNetCore.svg?style=for-the-badge)](https://www.nuget.org/packages/ProfileSharp.AspNetCore/)|
|**ProfileSharp.NServiceBus**|![](https://img.shields.io/nuget/dt/ProfileSharp.NServiceBus?style=for-the-badge)|[![Nuget](https://img.shields.io/nuget/v/ProfileSharp.NServiceBus.svg?style=for-the-badge)](https://www.nuget.org/packages/ProfileSharp.NServiceBus/)|
|**ProfileSharp.Store.FileStore**|![](https://img.shields.io/nuget/dt/ProfileSharp.Store.FileStore?style=for-the-badge)|[![Nuget](https://img.shields.io/nuget/v/ProfileSharp.Store.FileStore.svg?style=for-the-badge)](https://www.nuget.org/packages/ProfileSharp.Store.FileStore/)|
