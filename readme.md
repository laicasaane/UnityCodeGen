# UnityCodeGen

A code generation library for Unity. Based on [Vengarioth/UnityCodeGen](https://github.com/Vengarioth/UnityCodeGen)

## Example

```csharp
var builder = new AstBuilder();

builder.WithUsing()
    .WithNamespaceName("System");

var namespaceBuilder = builder.WithNamespace()
    .WithName("TestNamespace");

var classBuilder = namespaceBuilder.WithClass()
    .WithName("FooBar")
    .WithVisibility(AccessType.Public)
    .IsPartial(true);

classBuilder.WithProperty()
    .WithName("Foo")
    .WithType("int")
    .WithVisibility(AccessType.Public)
    .WithSetVisibility(AccessType.Public);

classBuilder.WithProperty()
    .WithName("Bar")
    .WithType("int")
    .WithVisibility(AccessType.Public)
    .WithSetVisibility(AccessType.Private);


classBuilder = namespaceBuilder.WithClass()
    .WithName("FooBar")
    .WithVisibility(AccessType.Public)
    .IsPartial(true);

var methodBuilder = classBuilder.WithMethod()
    .WithVisibility(AccessType.Public)
    .WithReturnType("void")
    .WithName("Serialize");

methodBuilder.WithParameter()
    .WithName("buffer")
    .WithType("IWriteableBuffer");

methodBuilder.WithParameter()
    .WithName("anotherBuffer")
    .WithType("IWriteableBuffer");

methodBuilder = classBuilder.WithMethod()
    .WithVisibility(AccessType.Public)
    .WithReturnType("void")
    .WithName("Deserialize");

methodBuilder.WithParameter()
    .WithName("buffer")
    .WithType("IReadableBuffer");

var result = new CSharpRenderer().Render(ast);
```

Results in

```csharp
using System;

namespace TestNamespace
{
    public partial class FooBar
    {
        public int Foo { get; set; }
        public int Bar { get; private set; }
    }

    public partial class FooBar
    {
        public void Serialize(IWriteableBuffer buffer, IWriteableBuffer anotherBuffer)
        {
            // TODO method body
        }

        public void Deserialize(IReadableBuffer buffer)
        {
            // TODO method body
        }

    }

}

```
