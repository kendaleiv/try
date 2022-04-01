// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Playwright;

using Xunit;

namespace Microsoft.TryDotNet.IntegrationTests;


public class WasmRunnerTests : PlaywrightTestBase
{

    [Fact]
    public async Task can_load_wasmrunner()
    {
        var page = await Playwright.Browser!.NewPageAsync();
        await page.GotoAsync(TryDotNet.Url + "wasmrunner");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator(@"id=wasmRunner-sentinel").IsHiddenAsync();

        await page.TestScreenShotAsync();
    }

    [Fact]
    public async Task can_run_assembly()
    {
        var page = await Playwright.Browser!.NewPageAsync();
        await page.GotoAsync(TryDotNet.Url + "wasmrunner");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator(@"id=wasmRunner-sentinel").IsHiddenAsync();

        var messages = await page.ExecuteAssembly(
            @"TVqQAAMAAAAEAAAA//8AALgAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAA4fug4AtAnNIbgBTM0hVGhpcyBwcm9ncmFtIGNhbm5vdCBiZSBydW4gaW4gRE9TIG1vZGUuDQ0KJAAAAAAAAABQRQAATAECAG+WAqcAAAAAAAAAAOAAIiALATAAAAYAAAACAAAAAAAAYiUAAAAgAAAAQAAAAAAAEAAgAAAAAgAABAAAAAAAAAAEAAAAAAAAAABgAAAAAgAAAAAAAAMAQIUAABAAABAAAAAAEAAAEAAAAAAAABAAAAAAAAAAAAAAABAlAABPAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAwAAAD0JAAAHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAACAAAAAAAAAAAAAAACCAAAEgAAAAAAAAAAAAAAC50ZXh0AAAAaAUAAAAgAAAABgAAAAIAAAAAAAAAAAAAAAAAACAAAGAucmVsb2MAAAwAAAAAQAAAAAIAAAAIAAAAAAAAAAAAAAAAAABAAABCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABEJQAAAAAAAEgAAAACAAUAcCAAAIQEAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADYAcgEAAHAoCgAACgAqIgIoCwAACgAqIgIoCwAACgAqQlNKQgEAAQAAAAAADAAAAHY0LjAuMzAzMTkAAAAABQBsAAAAhAEAACN+AADwAQAA2AEAACNTdHJpbmdzAAAAAMgDAAAcAAAAI1VTAOQDAAAQAAAAI0dVSUQAAAD0AwAAkAAAACNCbG9iAAAAAAAAAAIAAAFHFAAACQAAAAD6ATMAFgAAAQAAAAwAAAADAAAAAwAAAAsAAAAJAAAAAQAAAAEAAAAAAEoBAQAAAAAABgDaAKIBBgAsAaIBBgBNAI8BDwDCAQAABgATAXEBBgC7AHEBBgB4AHEBBgCVAHEBBgD6AHEBBgBhAHEBBgDRAWUBBgAdAGUBAAAAAAgAAAAAAAEAAQAAABAAXQGDAS0AAQABAAEAEAABADQALQABAAMAUCAAAAAAkQBsASMAAQBeIAAAAACGGIkBBgABAGcgAAAAAIYYiQEGAAEACQCJAQEAEQCJAQYAGQCJAQoAKQCJARAAMQCJARAAOQCJARAAQQCJARAASQCJARAAUQCJARAAYQBDABUAWQCJAQYALgALACcALgATADAALgAbAE8ALgAjAFgALgArAGwALgAzAHcALgA7AIQALgBDAFgALgBLAFgABIAAAAEAAAAAAAAAAAAAAAAAJQAAAAIAAAAAAAAAAAAAABoAEQAAAAAAAAAAAABDbGFzczEAPE1vZHVsZT4AbmV0c3RhbmRhcmQAQ29uc29sZQBibGF6b3ItY29uc29sZQBibGF6b3JfY29uc29sZQBXcml0ZUxpbmUARGVidWdnYWJsZUF0dHJpYnV0ZQBBc3NlbWJseVRpdGxlQXR0cmlidXRlAEFzc2VtYmx5RmlsZVZlcnNpb25BdHRyaWJ1dGUAQXNzZW1ibHlJbmZvcm1hdGlvbmFsVmVyc2lvbkF0dHJpYnV0ZQBBc3NlbWJseUNvbmZpZ3VyYXRpb25BdHRyaWJ1dGUAQ29tcGlsYXRpb25SZWxheGF0aW9uc0F0dHJpYnV0ZQBBc3NlbWJseVByb2R1Y3RBdHRyaWJ1dGUAQXNzZW1ibHlDb21wYW55QXR0cmlidXRlAFJ1bnRpbWVDb21wYXRpYmlsaXR5QXR0cmlidXRlAGJsYXpvci1jb25zb2xlLmRsbABQcm9ncmFtAFN5c3RlbQBNYWluAFN5c3RlbS5SZWZsZWN0aW9uAG15QXBwAC5jdG9yAFN5c3RlbS5EaWFnbm9zdGljcwBTeXN0ZW0uUnVudGltZS5Db21waWxlclNlcnZpY2VzAERlYnVnZ2luZ01vZGVzAE9iamVjdAAAGUgAZQBsAGwAbwAgAFcAbwByAGwAZAAhAAAAXQ9GanmtTEuzgc0NDhYR8wAEIAEBCAMgAAEFIAEBEREEIAEBDgQAAQEOCMx7E//NLd1RAwAAAQgBAAgAAAAAAB4BAAEAVAIWV3JhcE5vbkV4Y2VwdGlvblRocm93cwEIAQAHAQAAAAATAQAOYmxhem9yLWNvbnNvbGUAAAoBAAVEZWJ1ZwAADAEABzEuMC4wLjAAAAoBAAUxLjAuMAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAA4JQAAAAAAAAAAAABSJQAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARCUAAAAAAAAAAAAAAABfQ29yRGxsTWFpbgBtc2NvcmVlLmRsbAAAAAAA/yUAIAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAADAAAAGQ1AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="
            );

        await page.TestScreenShotAsync();

        messages.Should()
            .ContainSingle(m => m.type == "wasmRunner-result")
            .Which
            .result!.success.Should().Be(true);
    }

    [Fact]
    public async Task can_run_assembly_and_produce_output()
    {
        var page = await Playwright.Browser!.NewPageAsync();
        await page.GotoAsync(TryDotNet.Url + "wasmrunner");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator(@"id=wasmRunner-sentinel").IsHiddenAsync();

        var messages = await page.ExecuteAssembly(
            @"TVqQAAMAAAAEAAAA//8AALgAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAA4fug4AtAnNIbgBTM0hVGhpcyBwcm9ncmFtIGNhbm5vdCBiZSBydW4gaW4gRE9TIG1vZGUuDQ0KJAAAAAAAAABQRQAATAECAG+WAqcAAAAAAAAAAOAAIiALATAAAAYAAAACAAAAAAAAYiUAAAAgAAAAQAAAAAAAEAAgAAAAAgAABAAAAAAAAAAEAAAAAAAAAABgAAAAAgAAAAAAAAMAQIUAABAAABAAAAAAEAAAEAAAAAAAABAAAAAAAAAAAAAAABAlAABPAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAwAAAD0JAAAHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAACAAAAAAAAAAAAAAACCAAAEgAAAAAAAAAAAAAAC50ZXh0AAAAaAUAAAAgAAAABgAAAAIAAAAAAAAAAAAAAAAAACAAAGAucmVsb2MAAAwAAAAAQAAAAAIAAAAIAAAAAAAAAAAAAAAAAABAAABCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABEJQAAAAAAAEgAAAACAAUAcCAAAIQEAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADYAcgEAAHAoCgAACgAqIgIoCwAACgAqIgIoCwAACgAqQlNKQgEAAQAAAAAADAAAAHY0LjAuMzAzMTkAAAAABQBsAAAAhAEAACN+AADwAQAA2AEAACNTdHJpbmdzAAAAAMgDAAAcAAAAI1VTAOQDAAAQAAAAI0dVSUQAAAD0AwAAkAAAACNCbG9iAAAAAAAAAAIAAAFHFAAACQAAAAD6ATMAFgAAAQAAAAwAAAADAAAAAwAAAAsAAAAJAAAAAQAAAAEAAAAAAEoBAQAAAAAABgDaAKIBBgAsAaIBBgBNAI8BDwDCAQAABgATAXEBBgC7AHEBBgB4AHEBBgCVAHEBBgD6AHEBBgBhAHEBBgDRAWUBBgAdAGUBAAAAAAgAAAAAAAEAAQAAABAAXQGDAS0AAQABAAEAEAABADQALQABAAMAUCAAAAAAkQBsASMAAQBeIAAAAACGGIkBBgABAGcgAAAAAIYYiQEGAAEACQCJAQEAEQCJAQYAGQCJAQoAKQCJARAAMQCJARAAOQCJARAAQQCJARAASQCJARAAUQCJARAAYQBDABUAWQCJAQYALgALACcALgATADAALgAbAE8ALgAjAFgALgArAGwALgAzAHcALgA7AIQALgBDAFgALgBLAFgABIAAAAEAAAAAAAAAAAAAAAAAJQAAAAIAAAAAAAAAAAAAABoAEQAAAAAAAAAAAABDbGFzczEAPE1vZHVsZT4AbmV0c3RhbmRhcmQAQ29uc29sZQBibGF6b3ItY29uc29sZQBibGF6b3JfY29uc29sZQBXcml0ZUxpbmUARGVidWdnYWJsZUF0dHJpYnV0ZQBBc3NlbWJseVRpdGxlQXR0cmlidXRlAEFzc2VtYmx5RmlsZVZlcnNpb25BdHRyaWJ1dGUAQXNzZW1ibHlJbmZvcm1hdGlvbmFsVmVyc2lvbkF0dHJpYnV0ZQBBc3NlbWJseUNvbmZpZ3VyYXRpb25BdHRyaWJ1dGUAQ29tcGlsYXRpb25SZWxheGF0aW9uc0F0dHJpYnV0ZQBBc3NlbWJseVByb2R1Y3RBdHRyaWJ1dGUAQXNzZW1ibHlDb21wYW55QXR0cmlidXRlAFJ1bnRpbWVDb21wYXRpYmlsaXR5QXR0cmlidXRlAGJsYXpvci1jb25zb2xlLmRsbABQcm9ncmFtAFN5c3RlbQBNYWluAFN5c3RlbS5SZWZsZWN0aW9uAG15QXBwAC5jdG9yAFN5c3RlbS5EaWFnbm9zdGljcwBTeXN0ZW0uUnVudGltZS5Db21waWxlclNlcnZpY2VzAERlYnVnZ2luZ01vZGVzAE9iamVjdAAAGUgAZQBsAGwAbwAgAFcAbwByAGwAZAAhAAAAXQ9GanmtTEuzgc0NDhYR8wAEIAEBCAMgAAEFIAEBEREEIAEBDgQAAQEOCMx7E//NLd1RAwAAAQgBAAgAAAAAAB4BAAEAVAIWV3JhcE5vbkV4Y2VwdGlvblRocm93cwEIAQAHAQAAAAATAQAOYmxhem9yLWNvbnNvbGUAAAoBAAVEZWJ1ZwAADAEABzEuMC4wLjAAAAoBAAUxLjAuMAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAA4JQAAAAAAAAAAAABSJQAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARCUAAAAAAAAAAAAAAABfQ29yRGxsTWFpbgBtc2NvcmVlLmRsbAAAAAAA/yUAIAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAADAAAAGQ1AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="
            );

        await page.TestScreenShotAsync();

        messages.Should()
            .ContainSingle(m => m.type == "wasmRunner-stdout")
            .Which
            .message.Should().Be("Hello World!\n");

    }


    public WasmRunnerTests(PlaywrightFixture playwright, TryDotNetFixture tryDotNet) : base(playwright, tryDotNet)
    {
    }
}