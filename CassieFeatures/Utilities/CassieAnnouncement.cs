namespace CassieFeatures.Utilities
{
    public class CassieAnnouncement
    {
        public CassieAnnouncement()
        {
        }
        public CassieAnnouncement(string content, string subtitles, bool showSubtitles, bool isNoisy, float delay)
        {
            Content = content;
            Subtitles = subtitles;
            ShowSubtitles = showSubtitles;
            IsNoisy = isNoisy;
            Delay = delay;
        }
        
        public string Content { get; init; }
        public string Subtitles { get; init; }
        public bool ShowSubtitles { get; init; }
        public bool IsNoisy { get; init; }
        public float Delay { get; init; }
    }
}