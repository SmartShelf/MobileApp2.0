# Cross.Pie.Forms Details
---
## Description
Are you finding Well-Crafted Pie Chart Library for Xamarin.Forms ?

Cross.Pie.Forms is designed and crafted for you.

## Feature
- Easy to use.
- Beautiful UI.
- Full Features.
- Fully Customizable.
- Support Xamarin.Forms both iOS ,Android and Windows Phone.
- Every Property is Bindable Property.

##For Simplest usage

    var pie = new CrossPie();
    pie.AddItems(new PieItem { Title="First",Value=2});
    pie.AddItems(new PieItem { Title="Second",Value=5});
    pie.Update();

##CrossPie Properties
- Title - Show Title
- IsTitleOnTop - on top or bottom of chart.
- TitleColor
- BackgroundColor
- IsEnable - clickable events on chart
- IsPercentVisible - whether show percent
- IsValueVisible - whether show value
- Font
- TextColor
- PercentColor
- ValueColor
- SelectedItem - last clicked item
- IsSingleSelectable
- StartAngle - First Segment start angle
- PullRatio - pull length (0.1 = 10%)

##CrossPie Methods
- Add - Add a PieItem
- Update - When finish add all PieItem,Call this method to show chart
- Clear - remove all pieitem
- ClearAllPull - reset pull on every item
- AddRange - Add list of PieItem

##CrossPie Event
- ItemSelected(object sender,PieItem e)

##PieItem properties
- Title
- Value
- Color
- IsPull
- IsSelected
- IsBold
- PenWidth
- TitleColor
- PercentColor
- ValueColor
- Tag - for attaching any relevant object
