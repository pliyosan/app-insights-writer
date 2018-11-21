using System;
using System.Collections.Generic;

public class Arguments
{
    private const string INSTRUMENTATION_KEY = "INSTRUMENTATION_KEY";
    private const string SERVICE_NAME = "SERVICE_NAME";
    private const string COMPUTE_NAME = "COMPUTE_NAME";

    private const string LOCATION_NAME = "LOCATION_NAME";
    private const string VERSION_NUMBER = "VERSION_NUMBER";


    public static string InstrumentationKey()
    {
        return ReadMandatoryValue_FromEnvironmentVariables(INSTRUMENTATION_KEY);
    }

    public static string ServiceName()
    {
        return ReadMandatoryValue_FromEnvironmentVariables(SERVICE_NAME);
    }

    public static Dictionary<string, string> CustomProperties()
    {
        var customProperties = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(ComputeName())) customProperties[COMPUTE_NAME] = ComputeName();
        if (!string.IsNullOrEmpty(LocationName())) customProperties[LOCATION_NAME] = LocationName();
        if (!string.IsNullOrEmpty(VersionNumber())) customProperties[VERSION_NUMBER] = VersionNumber();

        return customProperties;
    }

    private static string ComputeName()
    {
        return Environment.GetEnvironmentVariable(COMPUTE_NAME);
    }

    private static string LocationName()
    {
        return Environment.GetEnvironmentVariable(LOCATION_NAME);
    }

    private static string VersionNumber()
    {
        return Environment.GetEnvironmentVariable(VERSION_NUMBER);
    }

    private static string ReadMandatoryValue_FromEnvironmentVariables(string key)
    {
        if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
             throw new ArgumentException("Missing environment variable: " + key);
        return Environment.GetEnvironmentVariable(key);
    }
}