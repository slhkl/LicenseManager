using Utils.Helpers;

namespace Utils
{
    public class LicenseManager
    {
        private static LicenseManager instance;

        public static LicenseManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new LicenseManager();
                return instance;
            }
        }

        /// <summary>
        /// string: Create your own keys and add them as md5.
        /// Datetime: Expiry date
        /// </summary>
        private readonly IEnumerable<Tuple<string, DateTime>> Values = new List<Tuple<string, DateTime>>
        {
            //I used the ConvertToMD5 method to make the examples understandable,
            //but in order to avoid reverse engineering,
            //you can convert your key to md5 and add it here.
           new Tuple<string, DateTime>("NSE6-R8BA-G8QG-L5VU-L2A9".ConvertToMD5(), DateTime.Now),
           new Tuple<string, DateTime>("S8NN-99CK-KUL3-L38W-VEYB".ConvertToMD5(), DateTime.Now.AddMinutes(60)),
           new Tuple<string, DateTime>("KXZ9-R2L4-K4S9-CQDM-K9A9".ConvertToMD5(), DateTime.Now.AddHours(12)),
           new Tuple<string, DateTime>("KY7X-8LLB-3H8F-N4KG-D8JH".ConvertToMD5(), DateTime.Now.AddMonths(6)),
           new Tuple<string, DateTime>("ZY5V-X9EE-J4EL-AMC5-RLD0".ConvertToMD5(), DateTime.Now.AddYears(1))
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
            var key = Values.Where(l => l.Item1.Equals(licenseKey.ConvertToMD5()));
            var isValidKey = key.Any() && key.First().Item2 > DateTime.Now;

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
