# Nancy.Patch
Add support for patching models

NuGet package available [here](https://www.nuget.org/packages/Nancy.Patch/)


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
