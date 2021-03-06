using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using JetBrains.Annotations;
using Robust.Shared.ContentPack;
using Robust.Shared.Serialization;

namespace Robust.Shared.Localization
{
    // ReSharper disable once CommentTypo
    /// <summary>
    ///     Provides facilities to obtain language appropriate in-game text.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///     Translation is handled using Project Fluent (https://www.projectfluent.org/)
    ///     You pass a Fluent 'identifier' as a string and the localization manager will fetch the message
    ///     matching that identifier from the currently loaded language's Fluent files.
    ///     </para>
    /// </remarks>
    /// <seealso cref="Loc"/>
    [PublicAPI]
    public interface ILocalizationManager
    {
        /// <summary>
        ///     Gets a language approrpiate string represented by the supplied messageId.
        /// </summary>
        /// <param name="messageId">Unique Identifier for a translated message.</param>
        /// <returns>
        ///     The language appropriate message if available, otherwise the messageId is returned.
        /// </returns>
        string GetString(string messageId);

        bool TryGetString(string messageId, [NotNullWhen(true)] out string? value);

        /// <summary>
        ///     Version of <see cref="GetString(string)"/> that supports arguments.
        /// </summary>
        string GetString(string messageId, params (string, object)[] args);

        bool TryGetString(string messageId, [NotNullWhen(true)] out string? value, params (string, object)[] args);

        /// <summary>
        ///     Default culture used by other methods when no culture is explicitly specified.
        ///     Changing this also changes the current thread's culture.
        /// </summary>
        CultureInfo? DefaultCulture { get; set; }

        /// <summary>
        ///     Load data for a culture.
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="culture"></param>
        void LoadCulture(IResourceManager resourceManager, CultureInfo culture);




        /// <summary>
        /// Remnants of the old Localization system.
        /// It exists to prevent source errors and allow existing game text to *mostly* work
        /// </summary>
        [Obsolete]
        [StringFormatMethod("text")]
        string GetString(string text, params object[] args);
    }

    internal interface ILocalizationManagerInternal : ILocalizationManager
    {
        void AddLoadedToStringSerializer(IRobustMappedStringSerializer serializer);
    }
}
