using System;
using Microsoft.WindowsAzure.MobileServices;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using NinjaLocator.iOS.Views;

namespace NinjaLocator.iOS.ViewControllers
{
    public partial class GettingStartedViewController : DialogViewController
    {
        public event Action Done;
        private EntryElement _nickname, _group;

        public string Nickname { get { return _nickname.Value; } }
        public string GroupName { get { return _group.Value; } }
        public MobileServiceAuthenticationProvider AuthenticationProvider { get; private set; }

        public GettingStartedViewController()
            : base(UITableViewStyle.Grouped, null)
        {
            Root = new RootElement("Getting started") {
				new Section ("1. Pick your login provider", "We need this to recognize you when you change nick names and devices") {
					new StyledStringElement ("(none selected)", OnSelectLoginProviderTapped),
				},
				new Section ("2. Pick a nick name", "Pick a nick name your friends will recognize") {
                    CreateNicknameElement(),
				},
                new Section("3. Join a group", "Members of the same group will be able to see eachother on a map") {
                    CreateGroupElement()
                },
			};
        }

        private void OnSelectLoginProviderTapped()
        {
            var source = (StyledStringElement)Root[0][0];
            var view = new LoginProviders();
            view.Clicked += (sender, args) =>
                {
                    var selectedProvider = view.ButtonTitle(args.ButtonIndex);
                    source.Caption = selectedProvider;
                    source.Accessory = UITableViewCellAccessory.Checkmark;
                    ReloadData();
                    AuthenticationProvider = (MobileServiceAuthenticationProvider) args.ButtonIndex;
                    _nickname.BecomeFirstResponder(true);
                };
            view.ShowInView(View);
        }

        private EntryElement CreateNicknameElement()
        {
            _nickname = new EntryElement("Nickname", "ninja wannabe", null)
                {
                    AutocapitalizationType = UITextAutocapitalizationType.None,
                    AutocorrectionType = UITextAutocorrectionType.No,
                    ReturnKeyType = UIReturnKeyType.Next
                };
            return _nickname;
        }

        private EntryElement CreateGroupElement()
        {
            _group = new EntryElement("Group", "The Groupies", null)
                {
                    AutocapitalizationType = UITextAutocapitalizationType.Words,
                    AutocorrectionType = UITextAutocorrectionType.Yes,
                    ReturnKeyType = UIReturnKeyType.Go,
                    EntryEnded = (sender, args) => Done()
                };
            return _group;
        }
    }
}