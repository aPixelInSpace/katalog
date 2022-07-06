using System.Text;

namespace Tests;

[UsesVerify]
public class ApprovalTest
{
    [Fact]
    public Task ThirtyDays()
    {
        var fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader("a\n"));

        Program.Main(new string[] { });
        var output = fakeoutput.ToString();

        return Verifier.Verify(output);
    }
}