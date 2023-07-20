using Utils;

if (!LicenseManager.Instance.IsLicensed())
{
    Console.WriteLine("license key not entered or entered key is not valid");
    Console.WriteLine("You must enter a license key to continue using the product.");

    bool isContinue = true;
    do
    {
        string licenseKey = Console.ReadLine();
        if (LicenseManager.Instance.IsValid(licenseKey))
        {
            Console.WriteLine("The application has been successfully licensed.");
            isContinue = false;
        }
        else
        {
            Console.WriteLine("Invalid license key entered.");
            Console.WriteLine("Would you like to re-enter? (Y - N)");

            string decision = Console.ReadLine();
            if (decision.ToUpper().StartsWith("N"))
            {
                Console.WriteLine("The product is shutting down.");
                Environment.Exit(0);
            }
            else if (decision.ToUpper().StartsWith("Y"))
                Console.WriteLine("Enter the license key.");
            else
            {
                Console.WriteLine("invalid value was entered.");
                Console.WriteLine("The product is shutting down.");
                Environment.Exit(1);
            }
        }
    } while (isContinue);
}
else
{
    Console.WriteLine("The application's license check is successful.");
}

Console.WriteLine("Application started.");