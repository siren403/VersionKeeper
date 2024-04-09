using UnityEngine.UIElements;

namespace VersionKeeper.Editor
{
    public readonly struct ProjectSettingElements
    {
        public Label TitleLabel { get; private init; }

        public VisualElement ContentContainer { get; private init; }

        public static ProjectSettingElements Q(VisualElement element)
        {
            var titleLabel = element.Q<Label>("TitleLabel");
            var contentContainer = element.Q("ContentContainer");
            return new ProjectSettingElements()
            {
                TitleLabel = titleLabel,
                ContentContainer = contentContainer
            };
        }

        public void Deconstruct(out Label titleLabel, out VisualElement contentContainer)
        {
            titleLabel = TitleLabel;
            contentContainer = ContentContainer;
        }
    }
}