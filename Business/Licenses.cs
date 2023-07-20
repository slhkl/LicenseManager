using Utils.Helpers;

namespace Utils
{
    public class Licenses
    {
        private static Licenses instance;

        public static Licenses Instance
        {
            get
            {
                if (instance == null)
                    instance = new Licenses();
                return instance;
            }
        }

        /// <summary>
        /// Create your own keys and add them as md5. 
        /// </summary>
        private readonly IEnumerable<string> Values = new List<string>
        {
            //I used the ConvertToMD5 method to make the examples understandable,
            //but in order to avoid reverse engineering,
            //you can convert your key to md5 and add it here.
            "NSE6-R8BA-G8QG-L5VU-L2A9".ConvertToMD5(),
            "S8NN-99CK-KUL3-L38W-VEYB".ConvertToMD5(),
            "KXZ9-R2L4-K4S9-CQDM-K9A9".ConvertToMD5(),
            "KY7X-8LLB-3H8F-N4KG-D8JH".ConvertToMD5(),
            "ZY5V-X9EE-J4EL-AMC5-RLD0".ConvertToMD5(),
        };

        private readonly string licenseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LicenseKey.txt");

        private string GetLicense()
        {
            if (File.Exists(licenseFilePath))
                return File.ReadAllText(licenseFilePath);
            else
                File.Create(licenseFilePath);

            return string.Empty;
        }

        private string GetLicenseFromEnvironment()
        {
            return Environment.GetEnvironmentVariable("LicenseKey") ?? string.Empty;
        }

        public bool IsLicensed()
        {
            return IsValid(GetLicense()) || IsValid(GetLicenseFromEnvironment());
        }

        public bool IsValid(string licenseKey)
        {
            var isValidKey = Values.Any(l => l.Equals(licenseKey.ConvertToMD5()));

            if (isValidKey)
                SetLicense(licenseKey);

            return isValidKey;
        }

        public void SetLicense(string licenseKey)
        {
            File.WriteAllText(licenseFilePath, licenseKey);
        }
    }
}
