﻿namespace Machete
{
    /// <summary>
    /// Parses a text span into a series of text spans.
    /// </summary>
    public interface TextParser
    {
        /// <summary>
        /// Parses the specified <see cref="TextSpan"/> of the input text.
        /// </summary>
        /// <param name="text">The text to parse</param>
        /// <param name="span">The span within the text available for parsing</param>
        /// <returns></returns>
        Result<TextSpan, TextSpan> Parse(ParseText text, TextSpan span);
    }


    /// <summary>
    /// A parser that bridges the gap between a TextParser and a regular parser
    /// </summary>
    /// <typeparam name="TResult">The parser result type</typeparam>
    public interface TextParser<out TResult>
    {
        Result<TextCursor, TResult> Parse(TextCursor cursor);
    }
}