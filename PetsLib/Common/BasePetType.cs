using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetsLib.Common.States;
using PetsLib.Common.Exceptions;
using Microsoft.UI.Xaml.Media.Imaging;

namespace PetsLib.Common
{
    /// <summary>
    /// Abstract base class for all pet types in the WinUI environment.
    /// </summary>
    public abstract class BasePetType : IPetType
    {
        #region Static Members
        public static int Count { get; private set; } = 0;
        public static readonly string[] PossibleColors = [];
        #endregion

        #region Fields
        protected readonly Image _spriteElement;
        protected readonly Grid _collisionElement;
        protected readonly TextBlock _speechElement;
        protected readonly Grid _containerGrid;
        protected ISequenceTree _sequence;
        protected IState _currentState;
        protected string _currentStateEnum;
        protected IState? _holdState;
        protected string? _holdStateEnum;
        protected string _petRoot;
        protected double _floor;
        protected double _left;
        protected double _bottom;
        protected IPetType? _friend;
        protected string _name;
        protected double _speed;
        protected string _size;
        protected double _climbSpeed = 1.0;
        protected double _climbHeight = 100.0;
        protected double _fallSpeed = 5.0;
        protected StateContext? _stateContext;
        #endregion

        #region Properties
        public string Label { get; protected set; } = "base";

        public double Speed => _speed;
        public double ClimbSpeed => _climbSpeed;
        public double ClimbHeight => _climbHeight;
        public double FallSpeed => _fallSpeed;
        public double Bottom => _bottom;
        public double Left => _left;
        public double Width => _spriteElement.ActualWidth > 0 ? _spriteElement.ActualWidth : CalculateSpriteWidth(_size);
        public double Floor => _floor;
        public string Name => _name;
        public bool HasFriend => _friend != null;
        public IPetType? Friend => _friend;
        public string Size => _size;

        public bool IsMoving => _speed > 0; // Equivalent to !== PetSpeed.still

        public bool CanSwipe => !StateHelpers.IsStateAboveGround(_currentStateEnum);

        public bool CanChase => !StateHelpers.IsStateAboveGround(_currentStateEnum) && IsMoving;

        public virtual string Hello => " says hello 👋!";

        public virtual string Emoji => "🐶";

        public bool IsPlaying => 
            IsMoving && (_currentStateEnum == PetStates.RunRight || _currentStateEnum == PetStates.RunLeft);
        #endregion

        #region Constructor
        protected BasePetType(
            Image spriteElement,
            Grid collisionElement, 
            TextBlock speechElement,
            Grid containerGrid,
            string size,
            double left,
            double bottom,
            string petRoot,
            double floor,
            string name,
            double speed,
            StateContext? stateContext = null)
        {
            _spriteElement = spriteElement ?? throw new ArgumentNullException(nameof(spriteElement));
            _collisionElement = collisionElement ?? throw new ArgumentNullException(nameof(collisionElement));
            _speechElement = speechElement ?? throw new ArgumentNullException(nameof(speechElement));
            _containerGrid = containerGrid ?? throw new ArgumentNullException(nameof(containerGrid));
            _size = size;
            _petRoot = petRoot;
            _floor = floor;
            _left = left;
            _bottom = bottom;
            _name = name;
            _speed = RandomizeSpeed(speed);
            _stateContext = stateContext;

            // Initialize default sequence
            _sequence = new SequenceTree();
            
            InitializeSprite(size, left, bottom);
            
            _currentStateEnum = _sequence.StartingState;
            if (_stateContext != null)
            {
                _currentState = StateHelpers.ResolveState(_currentStateEnum, this, _stateContext);
            }
            else 
            {
                // Create a default context if none provided
                _stateContext = new StateContext(1920, 1080);
                _currentState = StateHelpers.ResolveState(_currentStateEnum, this, _stateContext);
            }

            // Increment static count
            Count++;
        }
        #endregion

        #region Initialization
        protected virtual void InitializeSprite(string petSize, double left, double bottom)
        {
            var spriteWidth = CalculateSpriteWidth(petSize);
            
            // Position the sprite element using Margin (since we're in a Grid)
            _spriteElement.HorizontalAlignment = HorizontalAlignment.Left;
            _spriteElement.VerticalAlignment = VerticalAlignment.Bottom;
            _spriteElement.Margin = new Thickness(left, 0, 0, bottom);
            _spriteElement.Width = spriteWidth;
            _spriteElement.Height = spriteWidth;

            // Position collision element  
            _collisionElement.HorizontalAlignment = HorizontalAlignment.Left;
            _collisionElement.VerticalAlignment = VerticalAlignment.Bottom;
            _collisionElement.Margin = new Thickness(left, 0, 0, bottom);
            _collisionElement.Width = spriteWidth;
            _collisionElement.Height = spriteWidth;

            // Position speech element
            _speechElement.HorizontalAlignment = HorizontalAlignment.Left;
            _speechElement.VerticalAlignment = VerticalAlignment.Bottom;
            _speechElement.Margin = new Thickness(left, 0, 0, bottom + spriteWidth);
            
            HideSpeechBubble();
        }

        protected virtual double CalculateSpriteWidth(string size)
        {
            return size switch
            {
                var s when s == PetSizes.Nano => 30.0,
                var s when s == PetSizes.Small => 40.0,
                var s when s == PetSizes.Medium => 55.0,
                var s when s == PetSizes.Large => 110.0,
                _ => 30.0
            };
        }

        protected virtual double RandomizeSpeed(double speed)
        {
            var random = new Random();
            var min = speed * 0.7;
            var max = speed * 1.3;
            return random.NextDouble() * (max - min) + min;
        }
        #endregion

        #region Positioning
        public void PositionBottom(double bottom)
        {
            _bottom = bottom;
            _spriteElement.Margin = new Thickness(_left, 0, 0, _bottom);
            RepositionAccompanyingElements();
        }

        public void PositionLeft(double left)
        {
            _left = left;
            _spriteElement.Margin = new Thickness(_left, 0, 0, _bottom);
            RepositionAccompanyingElements();
        }

        protected virtual void RepositionAccompanyingElements()
        {
            _collisionElement.Margin = new Thickness(_left, 0, 0, _bottom);
            _speechElement.Margin = new Thickness(_left, 0, 0, _bottom + CalculateSpriteWidth(_size));
        }
        #endregion

        #region State Management
        public PetInstanceState GetState()
        {
            return new PetInstanceState { CurrentStateLabel = _currentStateEnum };
        }

        public void RecoverState(PetInstanceState state)
        {
            _currentStateEnum = state.CurrentStateLabel ?? PetStates.SitIdle;
            if (_stateContext != null)
            {
                _currentState = StateHelpers.ResolveState(_currentStateEnum, this, _stateContext);
            }

            if (!StateHelpers.IsStateAboveGround(_currentStateEnum))
            {
                PositionBottom(Floor);
            }
        }

        public void RecoverFriend(IPetType friend)
        {
            _friend = friend;
        }

        protected virtual string ChooseNextState(string fromState)
        {
            var possibleNextStates = _sequence.SequenceStates
                .FirstOrDefault(s => s.State == fromState)?.PossibleNextStates;

            if (possibleNextStates == null || possibleNextStates.Count == 0)
            {
                throw new InvalidStateException(fromState, Label);
            }

            var random = new Random();
            var idx = random.Next(possibleNextStates.Count);
            return possibleNextStates[idx];
        }
        #endregion

        #region Actions
        public void Swipe()
        {
            if (_currentStateEnum == PetStates.Swipe)
                return;

            _holdState = _currentState;
            _holdStateEnum = _currentStateEnum;
            _currentStateEnum = PetStates.Swipe;
            
            if (_stateContext != null)
            {
                _currentState = StateHelpers.ResolveState(_currentStateEnum, this, _stateContext);
            }
            
            ShowSpeechBubble("👋");
        }

        public void Chase(BallState ballState)
        {
            _currentStateEnum = PetStates.Chase;
            if (_stateContext != null)
            {
                _currentState = new ChaseState(this, ballState, _stateContext);
            }
        }

        public bool MakeFriendsWith(IPetType friend)
        {
            _friend = friend;
            ShowSpeechBubble($"I'm now friends ❤️ with {friend.Name}");
            return true;
        }
        #endregion

        #region Speech
        public void ShowSpeechBubble(string message, int durationMs = 3000)
        {
            _speechElement.Text = message;
            _speechElement.Visibility = Visibility.Visible;
            
            _ = Task.Delay(durationMs).ContinueWith(_ => HideSpeechBubble());
        }

        public void HideSpeechBubble()
        {
            // _speechElement.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Animation and Rendering  
        protected virtual void FaceLeft()
        {
            // In WinUI, we can use RenderTransform for flipping
            var scaleTransform = new Microsoft.UI.Xaml.Media.ScaleTransform
            {
                ScaleX = -1,
                ScaleY = 1
            };
            _spriteElement.RenderTransform = scaleTransform;
        }

        protected virtual void FaceRight()
        {
            var scaleTransform = new Microsoft.UI.Xaml.Media.ScaleTransform
            {
                ScaleX = 1,
                ScaleY = 1
            };
            _spriteElement.RenderTransform = scaleTransform;
        }
        #endregion

        #region Frame Update
        public virtual void NextFrame()
        {
            if (_currentState.HorizontalDirection == HorizontalDirection.Left)
            {
                FaceLeft();
            }
            else if (_currentState.HorizontalDirection == HorizontalDirection.Right)
            {
                FaceRight();
            }

            // Only update animation if state changed
            if ($"{_petRoot}_{_currentState.SpriteLabel}_8fps.gif" != ((BitmapImage)_spriteElement.Source).UriSource.OriginalString)
                _spriteElement.Source = new BitmapImage(new Uri($"{_petRoot}_{_currentState.SpriteLabel}_8fps.gif", UriKind.RelativeOrAbsolute));

            // Check friend interactions
            if (HasFriend && _currentStateEnum != PetStates.ChaseFriend && IsMoving)
            {
                if (Friend?.IsPlaying == true && !StateHelpers.IsStateAboveGround(_currentStateEnum))
                {
                    if (_stateContext != null)
                    {
                        _currentState = StateHelpers.ResolveState(PetStates.ChaseFriend, this, _stateContext);
                        _currentStateEnum = PetStates.ChaseFriend;
                        return;
                    }
                }
            }

            var frameResult = _currentState.NextFrame();
            
            if (frameResult == FrameResult.StateComplete)
            {
                // Handle swipe recovery
                if (_holdState != null && _holdStateEnum != null)
                {
                    _currentState = _holdState;
                    _currentStateEnum = _holdStateEnum;
                    _holdState = null;
                    _holdStateEnum = null;
                    return;
                }

                var nextState = ChooseNextState(_currentStateEnum);
                if (_stateContext != null)
                {
                    _currentState = StateHelpers.ResolveState(nextState, this, _stateContext);
                    _currentStateEnum = nextState;
                }
            }
            else if (frameResult == FrameResult.StateCancel)
            {
                if (_currentStateEnum == PetStates.Chase || _currentStateEnum == PetStates.ChaseFriend)
                {
                    var nextState = ChooseNextState(PetStates.IdleWithBall);
                    if (_stateContext != null)
                    {
                        _currentState = StateHelpers.ResolveState(nextState, this, _stateContext);
                        _currentStateEnum = nextState;
                    }
                }
            }
        }
        #endregion

        #region Cleanup
        public virtual void Remove()
        {
            // Remove from parent container if needed
            if (_containerGrid.Children.Contains(_spriteElement))
            {
                _containerGrid.Children.Remove(_spriteElement);
            }
            if (_containerGrid.Children.Contains(_collisionElement))
            {
                _containerGrid.Children.Remove(_collisionElement);
            }
            if (_containerGrid.Children.Contains(_speechElement))
            {
                _containerGrid.Children.Remove(_speechElement);
            }
            
            Count--;
        }
        #endregion
    }
}