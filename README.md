# Ramboe.Blazor.UserFeedback

Ramboe UserFeedback - A Blazor library for showing spinners, error and success messages during asynchronous method calls. This framework simplifies the handling of asynchronous UI updates, allowing developers to focus on their core logic and providing a seamless user experience.   

With its easy-to-use API, this library makes it simple to display spinners, error messages, and success messages for asynchronous method calls. Give your users the feedback they need, without sacrificing simplicity or performance.  

## Add the nuget package to your blazor server / wasm project

```
dotnet add package Ramboe.Blazor.UserFeedback --version 1.0.0
```

## Functionality  

The basic functionality is to display a spinner while a function is executed  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F2l1wHgbTwz.png?alt=media&token=0e0bffad-42e8-45e7-b6ef-6dae7e03a2bb)  

and display any exceptions that might occur (UI remains still responsive)  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F2c5NsmkImH.png?alt=media&token=437fbae7-f1f3-4e15-b6bd-2c4afa8eef38)  

you can also display a loading message beneath the spinner  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FZS4TVSrgq5.png?alt=media&token=d21192d1-04e4-4c24-981a-e4e8b0ae851b)  

and optionally display a success message (e.g. for successful http posts)  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FtZ56dmq1Bo.png?alt=media&token=401357d8-d67b-4434-9073-928ef79eb320)  

## Anatomy  

### Areas  

First make your component inherit from TryWrapper  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FzWtVP1YC_U.png?alt=media&token=ab3e9234-67af-48cb-a02d-83674e5e9382)  

then wrap the FeedbackArea around the HTML markup that you want to hide while spinner is active  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FI_DFelmlHj.png?alt=media&token=5340cffb-e58c-4672-a9b1-ecca5a251007)  

the component's code should look like this  

```html
@inherits Ramboe.Blazor.UserFeedback.ComponentBaseExtensions.TryWrapper

<FeedbackArea Target="DefaultTarget">
    <table class="table">
        <thead>
        <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
        </tr>
        </thead>
        <tbody>
        @foreach (var forecast in forecasts)
        {
            <tr>
                <td>@forecast.Date.ToShortDateString()</td>
                <td>@forecast.TemperatureC</td>
                <td>@forecast.TemperatureF</td>
                <td>@forecast.Summary</td>
            </tr>
        }
        </tbody>
    </table>
</FeedbackArea>
```  

FeedbackArea takes several parameters if you need to customize the spinner for a certain area within your application  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FHGmuIHTnNw.png?alt=media&token=12a20a39-a05d-48a3-b72c-87375d57e6e4)  

### Targets  

In order to determine which area is to spin while we are executing stuff, we need to attach a FeedbackTarget to it, which is responsible for holding the state of the feedback area (spinner active, success message shown, etc..)  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F-2Lg9suyI5.png?alt=media&token=73f6bcc2-92f7-4a80-b34d-1672ef65bbd8)  

since we inherit from trywrapper, we always get at least one target that we can use, the DefaultTarget   

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2Fje3fePRW4J.png?alt=media&token=258bf2e0-4949-48e9-9b36-f09622f82be3)  

if you need more targets, simply add them to your component (don't forget to initialize with "new()")  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FNZ5u8IrzLA.png?alt=media&token=d1488069-e349-4e18-b5eb-67e31d1f3d98)  

## Usage  

### Setting up configuration on startup  

Feedback areas need to be configured at startup. You can use the AddStandardFeedbackAreas() method   

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F5QkbS6g5Ue.png?alt=media&token=fca818ed-5eae-4447-9ef8-61db5d91acf3)  

which applies the following configuration  

* SpinnerSizeMode = SpinnerSizeMode.**Vh5**,  

* ColorMode = ColorMode.**Primary**,  

* SpinnerType = SpinnerType.**BorderSpinner**  

you can also use fluent api that comes along with AddAndConfigureFeedbackAreas() if you want to configure the styling yourself  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F99d1bMQCqs.png?alt=media&token=3af005aa-c431-4321-9344-6e8b2de5c9cb)  

WithColor() also has an overload that works with the ColorMode enum  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FACh6sr-g4f.png?alt=media&token=f561915c-fc90-4ed2-b57a-3bb4150a6ef8)  

### Configuring the Spinner  

The following attributes can be configured: Size, Color, Type  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2F22zqzS8uEl.png?alt=media&token=46c4854a-6007-4f52-863e-e09bc7ff4c99)  

Size: configured through SpinnerSizeMode enum, which ranges from vh2 - vh90  

Color: configured through SpinnerColorMode enum, which represents the color categories that come with [bootstrap](https://getbootstrap.com/docs/4.2/components/spinners/colors) (primary, secondary, success, ...)  

Type: configured through SpinnerTypeMode enum, which currently contais [spinner-border](https://getbootstrap.com/docs/4.2/components/spinners/border-spinner) and [spinner-grow](https://getbootstrap.com/docs/4.2/components/spinners/growing-spinner)  

### Remember: parameters override startup config  

any parameters passed directly into a FeedbackArea will override any configuration done in Program.cs. For example setting "primary" color mode on FeedbackArea #2 ...  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FigVs19A83o.png?alt=media&token=633b4c04-e39a-4a89-b690-278560b64623)  

results in that FeedbackAreas spinner color being set to "primary", while FeedbackArea #1 just takes the startup configuration:  

![](https://firebasestorage.googleapis.com/v0/b/firescript-577a2.appspot.com/o/imgs%2Fapp%2Framboe%2FTxaoSEP9hW.png?alt=media&token=13c211cf-1279-44e1-bc32-40bf1fde81a3)  

### Finally: Making it spin  

It works the same way as EventCallbacks. If you want to have the spinner spinning while a certain method is executed, 2 steps are needed:  

1) declare the method as usual  

```c#
    async Task<List<WeatherForecast>> LoadData()
    {
        // simulate waiting for an api, so we can actually see the spinner spinning
        await Task.Delay(2000);

        return await ForecastService.GetForecastAsync(DateOnly.FromDateTime(DateTime.Now));
    }
```  

2) pass your method and it's according target simply into one of the "TryAsync" overloads  

```c#
  protected override async Task OnInitializedAsync()
    {
        forecasts = await TryAsync(LoadData, DefaultTarget);

        forecasts = await TryAsync(LoadData, DefaultTarget, "loading with loading message but without success message");

        forecasts = await TryAsync(LoadData, DefaultTarget, "loading with success message", "updating data successful");
                
    }
```  

you can also use one of the explicit methods  

```c#
  protected override async Task OnInitializedAsync()
    {        
        // those to methods do the same thing

        forecasts = await TryAsync(LoadData, DefaultTarget, "loading with success message", "updating data successful");
    
        forecasts = await TryWithLoadingAndSuccessMessageAsync(LoadData, DefaultTarget, "loading with success message, explicit", "updating data successful")
    }
```  

Since it works the same way as EventCallbacks, you know already that you need a Lambda expression for Methods that take parameters:  

```c#
  protected override async Task OnInitializedAsync()
    {        
        await LambdaExample();
    }

  async Task LambdaExample()
    {
        await Task.Delay(2000);

        forecasts = await TryAsync(
            () => ForecastService.GetForecastAsync(DateOnly.FromDateTime(DateTime.Now)),
            DefaultTarget,
            "loading, lambda",
            "lambda successfull"
        );
    }
```  

## Summary  

configure FeedbackAreas on startup:  

```c#
builder.Services.AddAndConfigureFeedbackAreas()
       .WithSize(SpinnerSizeMode.Vh5)
       .WithType(SpinnerTypeMode.BorderSpinner)
       .WithColor(SpinnerColorMode.Secondary)
    .ConfigureFeedbackAreas();
```  

Inherit from TryWrapper and wrap a FeedbackArea (including target) around your content  

```html
@inherits Ramboe.Blazor.UserFeedback.ComponentBaseExtensions.TryWrapper

<FeedbackArea Target="DefaultTarget">
    @*Content here*@
</FeedbackArea>
```  

 You can use the various overloads..  

```c#
forecasts = await TryAsync(LoadData, DefaultTarget);

forecasts = await TryAsync(LoadData, DefaultTarget, "loading with loading message but without success message");

forecasts = await TryAsync(LoadData, DefaultTarget, "loading with success message", "updating data successful");
```  

...or be explicit  

```c#
  protected override async Task OnInitializedAsync()
    {
        forecasts = await TryWithLoadingAndSuccessMessageAsync(LoadData, DefaultTarget, "loading with success message, explicit", "updating data successful")
              
    }
```  

Methods are passed into FeedbackArea as delegates, meaning Lambda expressions are needed when working with parameters (just as in EventCallbacks).  
