using com.dke.data.agrirouter.api.dto.onboard;
using com.dke.data.agrirouter.api.env;
using Serilog;

namespace com.dke.data.agrirouter.api.test.service
{
    public class AbstractIntegrationTest
    {
        private int _testStep = 1;

        protected static string AccountId => "5d47a537-9455-410d-aa6d-fbd69a5cf990";

        protected static string ApplicationId => "39d18ae2-04e3-42de-8a42-935565a6b261";

        protected static string CertificationVersionId => "719afec8-d2ff-4cf8-8194-e688ae56b3b5";

        protected Environment Environment => new QA();

        protected AbstractIntegrationTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();
        }

        protected void LogTestStep(string message)
        {
            Log.Debug("******************************************************************************");
            Log.Debug($"* [{++_testStep}]: {message}");
            Log.Debug("******************************************************************************");
        }

        protected void LogDebugInformation(OnboardingResponse onboardingResponse)
        {
            Log.Debug("******************************************************************************");
            Log.Debug($"* [ACCOUNT_ID]: {AccountId}");
            Log.Debug($"* [APPLICATION_ID]: {ApplicationId}");
            Log.Debug($"* [CERTIFICATION_VERSION_ID]: {ApplicationId}");
            Log.Debug($"* [SENSOR_ALTERNATE_ID]: {onboardingResponse.SensorAlternateId}");
            Log.Debug($"* [DEVICE_ALTERNATE_ID]: {onboardingResponse.DeviceAlternateId}");
            Log.Debug("******************************************************************************");
        }
    }
}