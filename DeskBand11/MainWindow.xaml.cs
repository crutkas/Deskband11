using CommunityToolkit.Mvvm.Messaging;
using Microsoft.CmdPal.UI.Helpers;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PetsLib;
using PetsLib.Common;
using PetsLib.Common.Pets;
using PetsLib.Common.States;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DeskBand11
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx,
        IRecipient<OpenSettingsMessage>,
        IRecipient<QuitMessage>
    {
        private readonly HWND _hwnd;
        private readonly TrayIconService _trayIconService = new();
        private readonly PetCollection _petCollection = new();
        private readonly DispatcherTimer _frameTimer = new();
        private readonly Random _random = new();
        private StateContext? _stateContext;

        public MainWindow()
        {
            InitializeComponent();
            _hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(this).ToInt32());

            this.VisibilityChanged += MainWindow_VisibilityChanged;
            // this.ItemsBar.SizeChanged += ItemsBar_SizeChanged;
            this.Root.SizeChanged += ItemsBar_SizeChanged;

            WeakReferenceMessenger.Default.Register<OpenSettingsMessage>(this);
            WeakReferenceMessenger.Default.Register<QuitMessage>(this);
            MoveToTaskbar();
            _trayIconService.SetupTrayIcon(true);
            InitializeFrameTimer();
        }

        private void InitializeFrameTimer()
        {
            // Set up frame timer for 30 FPS
            _frameTimer.Interval = TimeSpan.FromMilliseconds(1000.0 / 30.0);
            _frameTimer.Tick += FrameTimer_Tick;
        }

        private void PetPlayground_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaygroundClip();

            MainContent.SizeChanged += (_, __) => UpdatePlaygroundClip();

            // Create state context based on playground dimensions
            _stateContext = new StateContext(
                (int)MainContent.ActualWidth,
                (int)MainContent.ActualHeight);

            // Create two dogs
            CreatePet("Buddy", PetColors.Brown, 50, 0);
            CreatePet("Max", PetColors.Black, 200, 0);

            UpdateStatusText($"Created 2 pets. Total: {_petCollection.Pets.Count}");

            // Start the animation loop
            _frameTimer.Start();

            // Periodically seek new friendships
            var friendshipTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            friendshipTimer.Tick += (_, __) => _petCollection.SeekNewFriends();
            friendshipTimer.Start();
        }

        private void UpdatePlaygroundClip()
        {
            double w = MainContent.ActualWidth;
            double h = MainContent.ActualHeight;

            if (w > 0 && h > 0)
            {
                PlaygroundClip.Rect = new Rect(0, 0, w, h);

                // Update state context when size changes
                if (_stateContext is not null)
                {
                    _stateContext = new StateContext(
                        (int)w,
                        (int)h);
                }
            }
        }

        private void CreatePet(string name, string color, double left, double bottom)
        {
            if (_stateContext is null) return;

            // Create UI elements for the pet
            var spriteImage = new Image
            {
                Source = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(
                    new Uri($"ms-appx:///Assets/pets/dog/{color}_walk_8fps.gif")),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            var collisionGrid = new Grid
            {
                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                    Microsoft.UI.Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            var speechTextBlock = new TextBlock
            {
                FontSize = 12,
                Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                    Microsoft.UI.Colors.Black),
                Padding = new Thickness(4),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Visibility = Visibility.Collapsed
            };

            // Create a border for the speech bubble background
            var speechBorder = new Border
            {
                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                    Microsoft.UI.Colors.White),
                CornerRadius = new CornerRadius(4),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Child = speechTextBlock,
                Visibility = Visibility.Collapsed
            };

            // Add elements to the playground
            MainContent.Children.Add(spriteImage);
            MainContent.Children.Add(collisionGrid);
            MainContent.Children.Add(speechBorder);

            // Create the dog pet instance
            var dog = new Dog(
                spriteImage,
                collisionGrid,
                speechTextBlock,
                MainContent,
                PetSizes.Medium,
                left,
                bottom,
                $"ms-appx:///Assets/pets/dog/{color}",
                0, // floor level
                name,
                PetSpeeds.Normal,
                _stateContext);

            // Create pet element and add to collection
            var petElement = new PetElement(
                spriteImage,
                collisionGrid,
                speechTextBlock,
                dog,
                color,
                PetTypes.Dog);

            _petCollection.Push(petElement);

            // Show greeting
            dog.ShowSpeechBubble($"Woof! I'm {name}!", 3000);
        }


        private void FrameTimer_Tick(object? sender, object e)
        {
            // Update all pets in the collection
            foreach (var petElement in _petCollection.Pets)
            {
                try
                {
                    petElement.Pet.NextFrame();
                }
                catch (Exception ex)
                {
                    // Log error and continue with other pets
                    System.Diagnostics.Debug.WriteLine($"Error updating pet {petElement.Pet.Name}: {ex.Message}");
                }
            }
        }

        private void UpdateStatusText(string message)
        {
            // StatusText.Text = message;
        }

        // Event handlers for UI buttons
        private void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            AddRandomPet();
            UpdateStatusText($"Added pet. Total: {_petCollection.Pets.Count}");
        }

        private void TriggerSwipeButton_Click(object sender, RoutedEventArgs e)
        {
            TriggerPetInteraction();
            UpdateStatusText("Triggered pet interaction");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _petCollection.Reset();
            MainContent.Children.Clear();
            UpdateStatusText("Reset all pets");
        }

        // Helper methods
        private void AddRandomPet()
        {
            if (_stateContext is null) return;

            var dogNames = Dog.DogNames;
            var colors = Dog.PossibleColors;

            var randomName = dogNames[_random.Next(dogNames.Length)];
            var randomColor = colors[_random.Next(colors.Length)];
            var randomX = _random.NextDouble() * Math.Max(MainContent.ActualWidth - 100, 50);

            CreatePet(randomName, randomColor, randomX, 0);
        }

        public void TriggerPetInteraction()
        {
            foreach (var petElement in _petCollection.Pets)
            {
                if (petElement.Pet.CanSwipe)
                {
                    petElement.Pet.Swipe();
                    return; // Just trigger one pet
                }
            }
        }

        private void ItemsBar_SizeChanged(object sender, Microsoft.UI.Xaml.SizeChangedEventArgs e)
        {
            ClipWindow();
        }

        private void MainWindow_VisibilityChanged(object sender, Microsoft.UI.Xaml.WindowVisibilityChangedEventArgs args)
        {
            MoveToTaskbar();
        }

        private void MoveToTaskbar()
        {
            ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            HWND thisWindow = _hwnd;

            HWND taskbarWindow = PInvoke.FindWindow("Shell_TrayWnd", null);
            HWND reBarWindow = PInvoke.FindWindowEx(taskbarWindow, HWND.Null, "ReBarWindow32", null);

            WINDOW_STYLE oldStyle = (WINDOW_STYLE)PInvoke.GetWindowLong(thisWindow, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
            WINDOW_STYLE oldStyleButNotPopup = oldStyle & (~WINDOW_STYLE.WS_POPUP);
            WINDOW_STYLE nowAddChild = oldStyleButNotPopup | WINDOW_STYLE.WS_CHILD;

            PInvoke.SetWindowLong(thisWindow, WINDOW_LONG_PTR_INDEX.GWL_STYLE, (int)nowAddChild);
            PInvoke.SetParent(thisWindow, taskbarWindow);

            RECT taskbarRect = new();
            PInvoke.GetWindowRect(taskbarWindow, out taskbarRect);

            RECT reBarRect = new();
            PInvoke.GetWindowRect(reBarWindow, out reBarRect);

            PInvoke.SetWindowPos(thisWindow,
                         HWND.Null,
                         taskbarRect.left,
                         reBarRect.top - taskbarRect.top,
                         taskbarRect.right - taskbarRect.left,
                         reBarRect.bottom - reBarRect.top,
                         0);

            ClipWindow();
        }

        private void ClipWindow()
        {
            FrameworkElement clipToElement = MainContent;
            System.Numerics.Vector2 clipToSize = clipToElement.ActualSize;
            Windows.Foundation.Point position = clipToElement.TransformToVisual(this.Content).TransformPoint(new());
            float scaleFactor = (float)this.GetDpiForWindow() / 96.0f;
            RECT scaledBounds = new()
            {
                left = (int)(position.X * scaleFactor),
                top = (int)(position.Y * scaleFactor),
                right = (int)((position.X + clipToElement.ActualWidth) * scaleFactor),
                bottom = (int)((position.Y + clipToElement.ActualHeight) * scaleFactor)
            };

            Debug.WriteLine($"ActualWidth: {clipToElement.ActualWidth}");
            Debug.WriteLine($"scaledBounds.Width: {scaledBounds.Width} ({scaledBounds.Width / scaleFactor})");

            PInvoke.SetWindowRgn(_hwnd,
                PInvoke.CreateRectRgn(scaledBounds.left,
                    scaledBounds.top, scaledBounds.right, scaledBounds.bottom),
                    true);
        }

        public void Receive(OpenSettingsMessage message)
        {
            // do nothing
        }

        public void Receive(QuitMessage message)
        {
            this.VisibilityChanged -= MainWindow_VisibilityChanged;
            this.Root.SizeChanged -= ItemsBar_SizeChanged;

            DispatcherQueue.TryEnqueue(() => Close());
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            _trayIconService.Destroy();
            Environment.Exit(0);
        }
        ~MainWindow()
        {
            _frameTimer?.Stop();
            _petCollection?.Reset();
        }
    }

    public record OpenSettingsMessage();
    public record QuitMessage();
}
