using Domain;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Well, that's my testing...
        // What did you expect? I'm lazy!
        // Ok, jokes aside, there will be unit tests which will be better than this.
        // But currently, there are none.

        var user = new User
        {
            Email = "test@test.com",
            Username = "test",
        };

        Assert.Equal("test@test.com", user.Email);
    }
}
