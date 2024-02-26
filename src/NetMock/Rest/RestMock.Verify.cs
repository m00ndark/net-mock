using System.Collections.Generic;
using System.Linq;
using NetMock.Exceptions;

namespace NetMock.Rest
{
	public partial class RestMock
	{
		public void VerifyGet(string path, Times times)
			=> Verify(Method.Get, path, new IMatch[0], times);

		public void VerifyGet(string path, IMatch match, Times times)
			=> Verify(Method.Get, path, new[] { match }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyGet(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Get, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyPost(string path, Times times)
			=> Verify(Method.Post, path, new IMatch[0], times);

		public void VerifyPost(string path, IMatch match, Times times)
			=> Verify(Method.Post, path, new[] { match }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyPost(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Post, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyPut(string path, Times times)
			=> Verify(Method.Put, path, new IMatch[0], times);

		public void VerifyPut(string path, IMatch match, Times times)
			=> Verify(Method.Put, path, new[] { match }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyPut(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Put, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyPatch(string path, Times times)
			=> Verify(Method.Patch, path, new IMatch[0], times);

		public void VerifyPatch(string path, IMatch match, Times times)
			=> Verify(Method.Patch, path, new[] { match }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyPatch(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Patch, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyDelete(string path, Times times)
			=> Verify(Method.Delete, path, new IMatch[0], times);

		public void VerifyDelete(string path, IMatch match, Times times)
			=> Verify(Method.Delete, path, new[] { match }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyDelete(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Delete, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyHead(string path, Times times)
			=> Verify(Method.Head, path, new IMatch[0], times);

		public void VerifyHead(string path, IMatch match, Times times)
			=> Verify(Method.Head, path, new[] { match }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyHead(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Head, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyOptions(string path, Times times)
			=> Verify(Method.Options, path, new IMatch[0], times);

		public void VerifyOptions(string path, IMatch match, Times times)
			=> Verify(Method.Options, path, new[] { match }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyOptions(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Options, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyTrace(string path, Times times)
			=> Verify(Method.Trace, path, new IMatch[0], times);

		public void VerifyTrace(string path, IMatch match, Times times)
			=> Verify(Method.Trace, path, new[] { match }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyTrace(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Trace, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void VerifyConnect(string path, Times times)
			=> Verify(Method.Connect, path, new IMatch[0], times);

		public void VerifyConnect(string path, IMatch match, Times times)
			=> Verify(Method.Connect, path, new[] { match }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void VerifyConnect(string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(Method.Connect, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void Verify(Method method, string path, Times times)
			=> Verify(method, path, new IMatch[0], times);

		public void Verify(Method method, string path, IMatch match, Times times)
			=> Verify(method, path, new[] { match }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, Times times)
			=> Verify(method, path, new[] { match1, match2 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, Times times)
			=> Verify(method, path, new[] { match1, match2, match3 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5, match6 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5, match6, match7 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9 }, times);

		public void Verify(Method method, string path, IMatch match1, IMatch match2, IMatch match3, IMatch match4, IMatch match5, IMatch match6, IMatch match7, IMatch match8, IMatch match9, IMatch match10, Times times)
			=> Verify(method, path, new[] { match1, match2, match3, match4, match5, match6, match7, match8, match9, match10 }, times);

		public void Verify(Method method, string path, IMatch[] matches, Times times)
		{
			RestRequestVerification requestVerification = new RestRequestVerification(this, method, path, matches);
			int matchCount = _receivedRequests.Count(request => requestVerification.Match(request, out _));

			if (!times.Condition(matchCount))
				throw new NetMockException($"Expected incoming calls to the mock {times.Description}, but was {matchCount} times");
		}
	}
}
