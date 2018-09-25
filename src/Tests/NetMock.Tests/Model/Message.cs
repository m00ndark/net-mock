namespace NetMock.Tests.Model
{
    public class Message
    {
	    public Message(string text = null)
	    {
		    Text = text;
	    }

	    public string Text { get; set; }
    }
}
