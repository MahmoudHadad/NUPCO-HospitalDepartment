// See https://aka.ms/new-console-template for more information

// Please, respect the order of the initialized objects and the order of assignments sinc they
// are all static and related - depending on each other - so, they have to be initialized and 
// and assigned in order!

// GIT Test

using AppService;

using Binding;

Binder.Bind();

static void Call()
{
    Console.WriteLine("```");

    Architecture.ArchitectBeta();

    string? value = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(value))
    {
        Call();
    }
}

Call();

