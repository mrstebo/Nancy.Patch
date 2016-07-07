[![Build status](https://ci.appveyor.com/api/projects/status/phemd9r0aebjl023?svg=true)](https://ci.appveyor.com/project/mrstebo/nancy-patch)

[![MyGet Prerelease](https://img.shields.io/myget/mrstebo/v/Nancy.Patch.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/mrstebo/package/nuget/Nancy.Patch) [![NuGet Version](https://img.shields.io/nuget/v/Nancy.Patch.svg)](https://www.nuget.org/packages/Nancy.Patch/)

# Nancy.Patch
Add support for patching models


### Example
Here is a basic example of how you can use `Nancy.Patch` in your projects
```cs
private dynamic PatchEntry(dynamic parameters)
{
    var customerId = (long) parameters.id;
    var customer = _repo.FindById(customerId);

    // Ensures only properties sent to the request are updated on the customer
    // e.g. { "name": "My New Name" }
    // will only update customer.Name and leave all other properties untouched
    if (!this.Patch(customer))
        return HttpStatusCode.UnprocessableEntity;

    _repo.Save(customer);

    return HttpStatusCode.NoContent;
}
```
