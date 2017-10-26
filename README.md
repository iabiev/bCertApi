# iabi.bCertApi

[![Build Status](https://jenkins.iabi.biz/buildStatus/icon?job=iabi.bCertApi.Tests)](https://jenkins.iabi.biz/job/iabi.bCertApi.Tests/)
[![NuGet](https://img.shields.io/nuget/v/iabi.bCertApi.svg)](https://www.nuget.org/packages/iabi.bCertApi)

This repository contains example code and the official C# client for the [b-Cert](https://www.b-cert.org) API.

Please also see the [official documentation on b-Cert](https://b-cert.org/Documentation/73904bbd-2cee-483e-fe6c-08d51c6aa458).

## API Specification

### Authentication

To authenticate in b-Cert, you can either use Http Basic Authentication or configure an Api Key. To use an Api Key, go to [your user profile in b-Cert](https://www.b-cert.org/Account/Manage) and create a key.
Only users that have either platform access or are part of a running certification have access to the API.

### List All Tests

    GET https://www.b-cert.org/Api/TestTool/Tests
    Response:
    [  
       {  
          "id":"6050f5ff-390e-43cc-25dd-08d40fc21b79",
          "name":"Beam-01A",
          "exchangeRequirementName":"Architectural Reference Exchange",
          "schemaName":"IFC4",
          "modelViewDefinitions":[  
             {  
                "id":"00000110-0899-0000-0000-000000000000",
                "name":"IFC4Add2RV_Beam_01A"
             }
          ]
       },
       {  
          "id":"f0b76fe7-5b7c-43e1-25e0-08d40fc21b79",
          "name":"Chimney-01A",
          "exchangeRequirementName":"Architectural Reference Exchange",
          "schemaName":"IFC4",
          "modelViewDefinitions":[  
             {  
                "id":"00000098-0851-0000-0000-000000000000",
                "name":"IFC4Add2RV_Chimney_01A"
             }
          ]
       }
    ]

### Check File

You can send an IFC4 file to the API and have it checked. This will not include any test specific checks but only
report general IFC4 schema and MVD compliance.

    POST https://www.b-cert.org/Api/TestTool/Check?jsonReport=true
    Form Parameter: ifcFile
    Response:
    {
        "Json": "<Report>"
    }

The report format depends on the current version of the check tool.

### Check File for Test

You can optionally include an `mvdId` parameter to perform checks against a specific test. The list of available mvds can be obtained at the `Tests` resource.
Test specific checks also support the output as Xml report.

    POST https://www.b-cert.org/Api/TestTool/Check?jsonReport=true&xmlReport=true&mvdId=00000110-0899-0000-0000-000000000000
    Form Parameter: ifcFile
    Response:
    {
        "Json": "<Report>",
        "Xml": "<Report>"
    }

The report format depends on the current version of the check tool.

## Using the console runner

The cross-platform console runner is available as binary download for Windows at the b-Cert documentation.

| Parameter | Alternative | Description |
|-----------|-------------|-------------|
| `-k` | `--apiKey` | Required. Your api key |
| `-t` | `--tests`  | Optional. Display all available tests in the console |
| `-i` | `--input`  | Optional. Path to the input IFC file |
| `-j` | `--json`   | Optional. Output path for the Json report |
| `-x` | `--xml`    | Optional. Output path for the Xml report |
| `-u` | `--uri`    | Optional. Different base Uri for b-Cert. Defaults to `https://www.b-cert.org` |
| `-m` | `--mvdId`  | Optional. Id of a Model View Definition to check against. If omitted, checks are run against the global MVD |

### List All Tests

    iabi.bCertApi.Console.exe -t -k <ApiKey>

    Available tests:
    Test: Beam-01A, Exchange Requirement: Architectural Reference Exchange
        Model View Definitions:
        IFC4RV_Beam_01A, Id: 00000110-0899-0000-0000-000000000000
    --------------------
    Test: Beam-01S, Exchange Requirement: Structural Reference Exchange
        Model View Definitions:
        IFC4RV_Beam_01S, Id: 00000070-0751-0000-0000-000000000000
    --------------------

### Check File

    iabi.bCertApi.Console.exe -k <ApiKey> -i input.ifc -j result.json

Will save the received result as `result.json`;

### Check File for Test

    iabi.bCertApi.Console.exe -k <ApiKey> -i input.ifc -j result.json -x result.xml -m 00000110-0899-0000-0000-000000000000
    
Will save the received results as `result.json` and `result.xml`.

## Using the .Net Package

The package is available at NuGet as `iabi.bCertApi`.