/* Generated by MyraPad at 6/11/2024 12:28:46 PM */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Properties;
using FontStashSharp.RichText;
using AssetManagementBase;

#if STRIDE
using Stride.Core.Mathematics;
#elif PLATFORM_AGNOSTIC
using System.Drawing;
using System.Numerics;
using Color = FontStashSharp.FSColor;
#else
// MonoGame/FNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace MUDMapBuilder.Editor.UI
{
	partial class MainForm: Panel
	{
		private void BuildUI()
		{
			_menuItemFileOpen = new MenuItem();
			_menuItemFileOpen.Text = "&Open";
			_menuItemFileOpen.ShortcutText = "Ctrl+O";
			_menuItemFileOpen.Id = "_menuItemFileOpen";

			_menuItemFileSave = new MenuItem();
			_menuItemFileSave.Text = "&Save";
			_menuItemFileSave.ShortcutText = "Ctrl+S";
			_menuItemFileSave.Id = "_menuItemFileSave";

			_menuItemFileSaveAs = new MenuItem();
			_menuItemFileSaveAs.Text = "S&ave As...";
			_menuItemFileSaveAs.Id = "_menuItemFileSaveAs";

			var menuItem1 = new MenuItem();
			menuItem1.Text = "&File";
			menuItem1.Items.Add(_menuItemFileOpen);
			menuItem1.Items.Add(_menuItemFileSave);
			menuItem1.Items.Add(_menuItemFileSaveAs);

			_menuHelpAbout = new MenuItem();
			_menuHelpAbout.Text = "&About";
			_menuHelpAbout.Id = "_menuHelpAbout";

			var menuItem2 = new MenuItem();
			menuItem2.Text = "&Help";
			menuItem2.Items.Add(_menuHelpAbout);

			_mainMenu = new HorizontalMenu();
			_mainMenu.Id = "_mainMenu";
			_mainMenu.Items.Add(menuItem1);
			_mainMenu.Items.Add(menuItem2);

			var label1 = new Label();
			label1.Text = "<<";

			_buttonStart = new Button();
			_buttonStart.Id = "_buttonStart";
			_buttonStart.Content = label1;

			_spinButtonStep = new SpinButton();
			_spinButtonStep.Maximum = 10000;
			_spinButtonStep.Minimum = 1;
			_spinButtonStep.Value = 1;
			_spinButtonStep.Integer = true;
			_spinButtonStep.Width = 80;
			_spinButtonStep.Id = "_spinButtonStep";

			var label2 = new Label();
			label2.Text = "To Compact";

			_buttonToCompact = new Button();
			_buttonToCompact.Id = "_buttonToCompact";
			_buttonToCompact.Content = label2;

			var label3 = new Label();
			label3.Text = ">>";

			_buttonEnd = new Button();
			_buttonEnd.Id = "_buttonEnd";
			_buttonEnd.Content = label3;

			var horizontalStackPanel1 = new HorizontalStackPanel();
			horizontalStackPanel1.Spacing = 8;
			horizontalStackPanel1.Widgets.Add(_buttonStart);
			horizontalStackPanel1.Widgets.Add(_spinButtonStep);
			horizontalStackPanel1.Widgets.Add(_buttonToCompact);
			horizontalStackPanel1.Widgets.Add(_buttonEnd);

			var label4 = new Label();
			label4.Text = "Remove Solitary Rooms";

			_checkRemoveSolitaryRooms = new CheckButton();
			_checkRemoveSolitaryRooms.Id = "_checkRemoveSolitaryRooms";
			_checkRemoveSolitaryRooms.Content = label4;

			var label5 = new Label();
			label5.Text = "Remove Rooms With Single Outside Exit";

			_checkRemoveRoomsWithSingleOutsideExit = new CheckButton();
			_checkRemoveRoomsWithSingleOutsideExit.Id = "_checkRemoveRoomsWithSingleOutsideExit";
			_checkRemoveRoomsWithSingleOutsideExit.Content = label5;

			var label6 = new Label();
			label6.Text = "Fix Obstacles";

			_checkFixObstacles = new CheckButton();
			_checkFixObstacles.IsChecked = true;
			_checkFixObstacles.Id = "_checkFixObstacles";
			_checkFixObstacles.Content = label6;

			var label7 = new Label();
			label7.Text = "Fix NonStraight";

			_checkFixNonStraight = new CheckButton();
			_checkFixNonStraight.IsChecked = true;
			_checkFixNonStraight.Id = "_checkFixNonStraight";
			_checkFixNonStraight.Content = label7;

			var label8 = new Label();
			label8.Text = "Fix Intersected";

			_checkFixIntersected = new CheckButton();
			_checkFixIntersected.IsChecked = true;
			_checkFixIntersected.Id = "_checkFixIntersected";
			_checkFixIntersected.Content = label8;

			var label9 = new Label();
			label9.Text = "Compact Map";

			_checkCompactMap = new CheckButton();
			_checkCompactMap.IsChecked = true;
			_checkCompactMap.Id = "_checkCompactMap";
			_checkCompactMap.Content = label9;

			var label10 = new Label();
			label10.Text = "Add Debug Info";

			_checkAddDebugInfo = new CheckButton();
			_checkAddDebugInfo.Id = "_checkAddDebugInfo";
			_checkAddDebugInfo.Content = label10;

			var label11 = new Label();
			label11.Text = "Colorize Connection Issues";

			_checkColorizeConnectionIssues = new CheckButton();
			_checkColorizeConnectionIssues.IsChecked = true;
			_checkColorizeConnectionIssues.Id = "_checkColorizeConnectionIssues";
			_checkColorizeConnectionIssues.Content = label11;

			var horizontalStackPanel2 = new HorizontalStackPanel();
			horizontalStackPanel2.Spacing = 8;
			horizontalStackPanel2.Widgets.Add(_checkRemoveSolitaryRooms);
			horizontalStackPanel2.Widgets.Add(_checkRemoveRoomsWithSingleOutsideExit);
			horizontalStackPanel2.Widgets.Add(_checkFixObstacles);
			horizontalStackPanel2.Widgets.Add(_checkFixNonStraight);
			horizontalStackPanel2.Widgets.Add(_checkFixIntersected);
			horizontalStackPanel2.Widgets.Add(_checkCompactMap);
			horizontalStackPanel2.Widgets.Add(_checkAddDebugInfo);
			horizontalStackPanel2.Widgets.Add(_checkColorizeConnectionIssues);

			_labelStatus = new Label();
			_labelStatus.Text = "Status";
			_labelStatus.TextColor = ColorStorage.CreateColor(3, 121, 255, 255);
			_labelStatus.Id = "_labelStatus";

			_panelMap = new ScrollViewer();
			_panelMap.Id = "_panelMap";

			_labelRoomsCount = new Label();
			_labelRoomsCount.Text = "Rooms: 10";
			_labelRoomsCount.TextColor = ColorStorage.CreateColor(3, 121, 255, 255);
			_labelRoomsCount.Id = "_labelRoomsCount";

			_labelGridSize = new Label();
			_labelGridSize.Text = "Grid Size: 10x10";
			_labelGridSize.TextColor = ColorStorage.CreateColor(3, 121, 255, 255);
			_labelGridSize.Id = "_labelGridSize";

			_labelStartCompactStep = new Label();
			_labelStartCompactStep.Text = "Start Compact Step: 100";
			_labelStartCompactStep.TextColor = ColorStorage.CreateColor(3, 121, 255, 255);
			_labelStartCompactStep.Id = "_labelStartCompactStep";

			_labelConnectionsWithObstacles = new Label();
			_labelConnectionsWithObstacles.Text = "Connections With Obstacles: 10";
			_labelConnectionsWithObstacles.TextColor = ColorStorage.CreateColor(254, 57, 48, 255);
			_labelConnectionsWithObstacles.Id = "_labelConnectionsWithObstacles";

			_labelNonStraightConnections = new Label();
			_labelNonStraightConnections.Text = "Non-Straight Connections: 10";
			_labelNonStraightConnections.TextColor = ColorStorage.CreateColor(255, 204, 1, 255);
			_labelNonStraightConnections.Id = "_labelNonStraightConnections";

			_labelIntersectedConnections = new Label();
			_labelIntersectedConnections.Text = "Intersected Connections: 10";
			_labelIntersectedConnections.TextColor = ColorStorage.CreateColor(87, 86, 213, 255);
			_labelIntersectedConnections.Id = "_labelIntersectedConnections";

			_labelLongConnections = new Label();
			_labelLongConnections.Text = "Long Connections: 10";
			_labelLongConnections.TextColor = ColorStorage.CreateColor(75, 217, 97, 255);
			_labelLongConnections.Id = "_labelLongConnections";

			_panelConnectionIssues = new VerticalStackPanel();
			_panelConnectionIssues.Spacing = 8;
			_panelConnectionIssues.Id = "_panelConnectionIssues";
			_panelConnectionIssues.Widgets.Add(_labelRoomsCount);
			_panelConnectionIssues.Widgets.Add(_labelGridSize);
			_panelConnectionIssues.Widgets.Add(_labelStartCompactStep);
			_panelConnectionIssues.Widgets.Add(_labelConnectionsWithObstacles);
			_panelConnectionIssues.Widgets.Add(_labelNonStraightConnections);
			_panelConnectionIssues.Widgets.Add(_labelIntersectedConnections);
			_panelConnectionIssues.Widgets.Add(_labelLongConnections);

			var panel1 = new Panel();
			StackPanel.SetProportionType(panel1, Myra.Graphics2D.UI.ProportionType.Fill);
			panel1.Widgets.Add(_panelMap);
			panel1.Widgets.Add(_panelConnectionIssues);

			var verticalStackPanel1 = new VerticalStackPanel();
			verticalStackPanel1.Spacing = 8;
			verticalStackPanel1.Widgets.Add(_mainMenu);
			verticalStackPanel1.Widgets.Add(horizontalStackPanel1);
			verticalStackPanel1.Widgets.Add(horizontalStackPanel2);
			verticalStackPanel1.Widgets.Add(_labelStatus);
			verticalStackPanel1.Widgets.Add(panel1);

			
			Widgets.Add(verticalStackPanel1);
		}

		
		public MenuItem _menuItemFileOpen;
		public MenuItem _menuItemFileSave;
		public MenuItem _menuItemFileSaveAs;
		public MenuItem _menuHelpAbout;
		public HorizontalMenu _mainMenu;
		public Button _buttonStart;
		public SpinButton _spinButtonStep;
		public Button _buttonToCompact;
		public Button _buttonEnd;
		public CheckButton _checkRemoveSolitaryRooms;
		public CheckButton _checkRemoveRoomsWithSingleOutsideExit;
		public CheckButton _checkFixObstacles;
		public CheckButton _checkFixNonStraight;
		public CheckButton _checkFixIntersected;
		public CheckButton _checkCompactMap;
		public CheckButton _checkAddDebugInfo;
		public CheckButton _checkColorizeConnectionIssues;
		public Label _labelStatus;
		public ScrollViewer _panelMap;
		public Label _labelRoomsCount;
		public Label _labelGridSize;
		public Label _labelStartCompactStep;
		public Label _labelConnectionsWithObstacles;
		public Label _labelNonStraightConnections;
		public Label _labelIntersectedConnections;
		public Label _labelLongConnections;
		public VerticalStackPanel _panelConnectionIssues;
	}
}
