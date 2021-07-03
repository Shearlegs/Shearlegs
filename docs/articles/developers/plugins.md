# Shearlegs Plugin concepts

## Parameters
### Adding Parameters
To add a parameters for your Shearlegs plugin you have to create a new class and make it inherit from `Shearlegs.Core.Plugins.Parameters`
```cs
using Shearlegs.Core.Plugins;

public class SampleParameters : Parameters 
{

}
```
You are allowed to have only one class in your project that inherits from `Shearlegs.Core.Plugins.Parameters`.  
Now you can add a properties for it. Every property by default is a parameter without default value, it's not required and has no description.  
To have a default value just assign the value for the property in the constructor

```cs
public class SampleParameters : Parameters 
{
    public string Text { get; set; } = "Hello World!";
}
```

To add the description and make the parameter required use the `Shearlegs.API.Plugins.Attributes.ParameterAttribute` on the property
```cs
public class SampleParameters : Parameters 
{

    public string Text { get; set; } = "Hello World!";
    [Parameter(IsRequired = true, Description = "The date of your birthday")]
    public DateTime Birthday { get; set; }
}
```

### Adding Secrets
To add secret simply add a parameter to your Parameters class and give it an attribute `Shearlegs.API.Plugins.Attributes.SecretAttribute`
```cs
public class SampleParameters : Parameters 
{
    [Secret]
    public string ConnectionString { get; set; }
}
```
Secrets cannot have a default value


### Reading Parameters & Secrets
Shearlegs creates an instance of your Parameters type and adds it to the dependency injection, so you can easily access the your Parameters instance from plugin or service class.
```cs
private readonly SampleParameters parameters;

public SamplePlugin(SampleParameters parameters)
{
    this.parameters = parameters;
}
```

## Results
### PluginTextResult

### PluginErrorResult



## Services

## ContentFileStore

### Adding IContentFile
To add file to IContentFileStore you have to add it to the project as EmbeddedResource.  
It will add this file in the execution to the IContentFileStore, so you can read it.
```xml
<ItemGroup>
  <EmbeddedResource Include="Template.xlsx" />
</ItemGroup>
```

### Reading IContentFile
To get IContentFile you first have to inject IContentFileStore to your plugin class or service
```cs
private readonly IContentFileStore fileStore;

public SamplePlugin(IContentFileStore fileStore)
{
    this.fileStore = fileStore;
}
```
Then to get a file inside the method use `fileStore.GetFile("Template.xlsx")` (specify file name in the parameter)
```cs
...
IContentFile file = fileStore.GetFile("template.xlsx"); // GetFile method is case insensitive
using ExcelPackage pckg = new(file.Content); // Content property returns Stream
...
```
Now you can access your embedded file Stream.