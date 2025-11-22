<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    x:Class="Gestion_intervention.MainPage"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:conv="clr-namespace:Gestion_intervention.Utilities.Converters"
    x:Name="RootPage"
    Padding="{OnPlatform iOS='0,24,0,0', Android='0'}"
    BackgroundColor="{AppThemeBinding Light=#F6F7FB, Dark=#151515}">

    <!-- =================== RESSOURCES =================== -->
    <ContentPage.Resources>
        <!-- Converters -->
        <conv:IsNullToTrueConverter x:Key="IsNullToTrueConverter" />
        <conv:EndVisibleMultiConverter x:Key="EndVisibleMultiConverter" />

        <!-- Couleurs -->
        <Color x:Key="Primary">#7B6EF6</Color>
        <Color x:Key="PrimaryDark">#6B5DE3</Color>
        <Color x:Key="SurfaceLight">#FFFFFF</Color>
        <Color x:Key="SurfaceDark">#1E1E1E</Color>
        <Color x:Key="BorderLight">#E6E6F0</Color>
        <Color x:Key="BorderDark">#2A2A2A</Color>
        <Color x:Key="TextSubtleLight">#6B7280</Color>
        <Color x:Key="TextSubtleDark">#9CA3AF</Color>

        <!-- Boutons actions -->
        <Style TargetType="Button" x:Key="ActionButton">
            <Setter Property="Padding" Value="{OnIdiom Phone='14,10', Tablet='18,10', Desktop='18,10'}"/>
            <Setter Property="CornerRadius" Value="16"/>
            <Setter Property="FontAttributes" Value="Bold"/>
            <Setter Property="FontSize" Value="14"/>
            <Setter Property="BackgroundColor" Value="{StaticResource Primary}"/>
            <Setter Property="TextColor" Value="White"/>
            <Setter Property="Shadow">
                <Setter.Value>
                    <Shadow Brush="#000000"
                            Opacity="0.18"
                            Radius="8"
                            Offset="0,3" />
                </Setter.Value>
            </Setter>
            <Setter Property="VisualStateManager.VisualStateGroups">
                <VisualStateGroupList>
                    <VisualStateGroup>
                        <VisualState x:Name="CommonStates">
                            <VisualState.Setters>
                                <Setter Property="BackgroundColor" Value="{StaticResource Primary}"/>
                            </VisualState.Setters>
                        </VisualState>
                        <VisualState x:Name="PointerOver">
                            <VisualState.Setters>
                                <Setter Property="BackgroundColor" Value="{StaticResource PrimaryDark}"/>
                            </VisualState.Setters>
                        </VisualState>
                        <VisualState x:Name="Disabled">
                            <VisualState.Setters>
                                <Setter Property="Opacity" Value="0.35"/>
                            </VisualState.Setters>
                        </VisualState>
                    </VisualStateGroup>
                </VisualStateGroupList>
            </Setter>
        </Style>

        <!-- Boutons Start/End -->
        <Style TargetType="Button" x:Key="RowPill">
            <Setter Property="Padding" Value="{OnIdiom Phone='10,6', Tablet='12,6', Desktop='12,6'}"/>
            <Setter Property="CornerRadius" Value="20"/>
            <Setter Property="FontAttributes" Value="Bold"/>
            <Setter Property="BackgroundColor" Value="{StaticResource Primary}"/>
            <Setter Property="TextColor" Value="White"/>
        </Style>

        <!-- En-têtes de colonnes -->
        <Style TargetType="Label" x:Key="HeaderLabel">
            <Setter Property="FontAttributes" Value="Bold"/>
            <Setter Property="TextColor" Value="{AppThemeBinding Light=#111, Dark=#EEE}"/>
        </Style>

        <!-- Cartes -->
        <Style TargetType="Border" x:Key="Card">
            <Setter Property="StrokeShape" Value="RoundRectangle 16"/>
            <Setter Property="Stroke" Value="{AppThemeBinding Light={StaticResource BorderLight}, Dark={StaticResource BorderDark}}"/>
            <Setter Property="Background" Value="{AppThemeBinding Light={StaticResource SurfaceLight}, Dark={StaticResource SurfaceDark}}"/>
            <Setter Property="Padding" Value="12"/>
        </Style>
    </ContentPage.Resources>

    <!-- =================== LAYOUT RACINE =================== -->
    <Grid RowSpacing="12"
          RowDefinitions="Auto,Auto,*,Auto">

        <!-- Logo de fond (TOUJOURS visible) -->
        <Image Source="logo_couleur.png"
               Grid.Row="2"
               Grid.RowSpan="2"
               Aspect="AspectFill"
               Opacity="1"
               InputTransparent="True"
               HorizontalOptions="Center"
               VerticalOptions="Center"
               WidthRequest="220"
               HeightRequest="325" />

        <!-- =================== ENTÊTE (logo + titre) =================== -->
        <!-- astuce: on met le titre en ColumnSpan=3 pour être VRAIMENT centré -->
        <Grid Grid.Row="0"
              ColumnDefinitions="Auto,*"
              Padding="{OnIdiom Phone='16,12', Tablet='16,12', Desktop='16,12'}"
              ColumnSpacing="12">
            <!-- Titre centré sur toute la largeur -->
            <Image Grid.Column="0"
                   Source="logo_barry_couleur.png"
                   HeightRequest="44"
                   VerticalOptions="Center"
                   HorizontalOptions="Start"
                   Margin="20,0,0,0"/>
        </Grid>

        <!-- =================== BARRE D’ACTIONS (mobile-style) =================== -->
        <Border Grid.Row="1"
                Background="{AppThemeBinding Light=#ECEEF4, Dark=#232323}"
                StrokeShape="RoundRectangle 18"
                Padding="12,10"
                Margin="16,0,16,4">
            <Border.Shadow>
                <Shadow Brush="#000000"
                        Opacity="0.12"
                        Radius="10"
                        Offset="0,3" />
            </Border.Shadow>

            <ScrollView Orientation="Horizontal"
                        HorizontalScrollBarVisibility="Never">
                <HorizontalStackLayout Spacing="12"
                                       Padding="3,0">
                    <Button Text="Add"
                            Style="{StaticResource ActionButton}"
                            Command="{Binding ShowAddInterventionPopupCommand}" 
                            BackgroundColor="Black"/>

                    <Button Text="Modify"
                            Style="{StaticResource ActionButton}"
                            Command="{Binding ShowEditInterventionPopupCommand}"
                            BackgroundColor="Black">
                            
                        <Button.Triggers>
                            <DataTrigger TargetType="Button"
                                         Binding="{Binding SelectedIntervention}"
                                         Value="{x:Null}">

                            </DataTrigger>
                        </Button.Triggers>
                    </Button>

                    <Button Text="Delete"
                            Style="{StaticResource ActionButton}"
                            Command="{Binding DeleteInterventionCommand}"
                            BackgroundColor="Black">
                        <Button.Triggers>
                            <DataTrigger TargetType="Button"
                                         Binding="{Binding SelectedIntervention}"
                                         Value="{x:Null}">
                            </DataTrigger>
                        </Button.Triggers>
                    </Button>

                    <Button Text="Details"
                            Style="{StaticResource ActionButton}"
                            Command="{Binding ShowInterventionDetailsCommand}"
                            CommandParameter="{Binding SelectedIntervention}"
                            BackgroundColor="Black">
                        <Button.Triggers>
                            <DataTrigger TargetType="Button"
                                         Binding="{Binding SelectedIntervention}"
                                         Value="{x:Null}">

                            </DataTrigger>
                        </Button.Triggers>
                    </Button>
                </HorizontalStackLayout>
            </ScrollView>
        </Border>

        <!-- =================== LISTE – PHONE =================== -->
        <Border Grid.Row="2"
                Style="{StaticResource Card}"
                Background="Transparent"
                Stroke="{x:Null}"
                Padding="0"
                Margin="16,0,16,4">
            <Grid RowDefinitions="Auto,Auto,*">
                <Border Grid.Row="0"
                        Background="{AppThemeBinding Light=#ECEEF4, Dark=#232323}"
                        StrokeShape="RoundRectangle 16"
                        Padding="14,10"
                        Margin="4,4,4,8">
                    <Grid ColumnDefinitions="*,Auto" ColumnSpacing="12">
                        <Label Text="Interventions"
                               FontAttributes="Bold"
                               FontSize="16"
                               TextColor="{AppThemeBinding Light=#111111, Dark=#EEEEEE}" />
                        <Label Grid.Column="1"
                               Text="Duration"
                               FontSize="12"
                               TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}"
                               HorizontalTextAlignment="End" />
                    </Grid>
                </Border>

                <BoxView Grid.Row="1"
                         HeightRequest="1"
                         Margin="4,0,4,8"
                         BackgroundColor="{AppThemeBinding Light={StaticResource BorderLight}, Dark={StaticResource BorderDark}}" />

                <CollectionView Grid.Row="2"
                                BackgroundColor="Transparent"
                                ItemsSource="{Binding Interventions}"
                                SelectionMode="Single"
                                SelectedItem="{Binding SelectedIntervention}"
                                ItemSizingStrategy="MeasureFirstItem"
                                ItemsLayout="VerticalList">
                    <CollectionView.EmptyView>
                        <Label Text="No intervention. Tap “Add”"
                               HorizontalOptions="Start"
                               VerticalOptions="Start"
                               Padding="24"
                               FontSize="14"
                               TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}" />
                    </CollectionView.EmptyView>

                    <CollectionView.ItemTemplate>
                        <DataTemplate>
                            <Border StrokeShape="RoundRectangle 18"
                                    Margin="8,6"
                                    Padding="12"
                                    Background="{AppThemeBinding Light={StaticResource SurfaceLight}, Dark={StaticResource SurfaceDark}}"
                                    Stroke="{AppThemeBinding Light={StaticResource BorderLight}, Dark={StaticResource BorderDark}}">
                                <Grid ColumnDefinitions="Auto,*"
                                      ColumnSpacing="12"
                                      RowDefinitions="Auto,Auto,Auto">
                                    <VerticalStackLayout Grid.RowSpan="3"
                                                         Spacing="8"
                                                         VerticalOptions="Center">
                                        <Button Text="Start"
                                                Style="{StaticResource RowPill}"
                                                BackgroundColor="#22C55E"
                                                Command="{Binding Source={x:Reference RootPage}, Path=BindingContext.StartInterventionCommand}"
                                                CommandParameter="{Binding .}">
                                            <Button.IsVisible>
                                                <Binding Path="StartTime" Converter="{StaticResource IsNullToTrueConverter}" />
                                            </Button.IsVisible>
                                        </Button>

                                        <Button Text="End"
                                                Style="{StaticResource RowPill}"
                                                BackgroundColor="#EF4444"
                                                Command="{Binding Source={x:Reference RootPage}, Path=BindingContext.EndInterventionCommand}"
                                                CommandParameter="{Binding .}">
                                            <Button.IsVisible>
                                                <MultiBinding Converter="{StaticResource EndVisibleMultiConverter}">
                                                    <Binding Path="StartTime" />
                                                    <Binding Path="EndTime" />
                                                </MultiBinding>
                                            </Button.IsVisible>
                                        </Button>
                                    </VerticalStackLayout>

                                    <Label Grid.Column="1"
                                           Grid.Row="0"
                                           Text="{Binding Name}"
                                           FontSize="16"
                                           FontAttributes="Bold"
                                           LineBreakMode="TailTruncation"
                                           TextColor="{AppThemeBinding Light=#1F2937, Dark=#F3F4F6}" />

                                    <Label Grid.Column="1"
                                           Grid.Row="1"
                                           Text="{Binding CategoryType}"
                                           FontSize="13"
                                           TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}" />

                                    <Grid Grid.Column="1"
                                          Grid.Row="2"
                                          ColumnDefinitions="*,*,Auto"
                                          ColumnSpacing="10">
                                        <Label Grid.Column="0"
                                               Text="{Binding StartTime, StringFormat='Start: {0:dd-MM HH\\:mm}'}"
                                               FontSize="12"
                                               TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}" />
                                        <Label Grid.Column="1"
                                               Text="{Binding EndTime, StringFormat='End: {0:dd-MM HH\\:mm}'}"
                                               FontSize="12"
                                               TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}" />
                                        <Border Grid.Column="2"
                                                StrokeShape="RoundRectangle 12"
                                                Padding="10,3"
                                                Background="{AppThemeBinding Light=#EEF2FF, Dark=#2A2A42}">
                                            <Label Text="{Binding Duration, StringFormat='{}{0:hh\\:mm}'}"
                                                   FontSize="12"
                                                   TextColor="{AppThemeBinding Light=#3949AB, Dark=#C7D2FE}" />
                                        </Border>
                                    </Grid>
                                </Grid>
                            </Border>
                        </DataTemplate>
                    </CollectionView.ItemTemplate>
                </CollectionView>
            </Grid>
        </Border>


        <!-- =================== PIED =================== -->
        <Grid Grid.Row="3"
              Padding="16,0,16,16"
              ColumnDefinitions="*,Auto">
            <Label Text="Stay productive wherever you are."
                   FontSize="12"
                   TextColor="{AppThemeBinding Light={StaticResource TextSubtleLight}, Dark={StaticResource TextSubtleDark}}"
                   VerticalTextAlignment="Center"/>
            <Border Grid.Column="1"
                    StrokeShape="RoundRectangle 14"
                    Background="{AppThemeBinding Light=#EEF2FF, Dark=#2A2A42}"
                    Padding="12,6">
                <HorizontalStackLayout Spacing="4">
                    <Label Text="Total" FontSize="12" TextColor="{AppThemeBinding Light=#3949AB, Dark=#C7D2FE}" />
                    <Label Text=":" FontSize="12" TextColor="{AppThemeBinding Light=#3949AB, Dark=#C7D2FE}" />
                    <Label Text="{Binding Interventions.Count}"
                           FontSize="14"
                           FontAttributes="Bold"
                           TextColor="{AppThemeBinding Light=#1F2937, Dark=#F9FAFB}" />
                </HorizontalStackLayout>
            </Border>
        </Grid>

    </Grid>
</ContentPage>
