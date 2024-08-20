using Domain.DbObjects;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Well, that's my testing .-.
        // What did you expect, im lazy!
        // Ok, jokes aside. There will be unit tests which will be (hopefully) better than this.
        // But currently there are none.

        var user = new User
        {
            Email = "test@test.com",
            Username = "test",
        };

        Assert.Equal("test@test.com", user.Email);
    }
}