   M S B A M L   `   `   `  ����?  ;PC, Version=9.27.17.1, Culture=neutral, PublicKeyToken=null#clr-namespace:PC.UtilsPC.Utils  5      -clr-namespace:PC.DataAccessPC.DataAccess  5      -clr-namespace:PC.ViewModelsPC.ViewModels  5          PC.Views.AllPC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (utilsclr-namespace:PC.Utils     5      2
DataAccessclr-namespace:PC.DataAccess     5      2
ViewModelsclr-namespace:PC.ViewModels     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5
      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6    � 	Resources 5       !  CustomMetroToggleSwitchButton + 'MahApps.Metro.Styles.ToggleSwitchButton �� BasedOn �� 
TargetType/  )MahApps.Metro.Controls.ToggleSwitchButton �� Property �� Value��%r   &	        7  �� 5      8   "  5      .�5      ��  �@ SwitchForeground
 �2͚�69   5      5      .+�5      )  #MahApps.Metro.Controls.ToggleSwitch  -   5          Name$ btn_toggle_edit��5   $   $��10 5 0 0q�5   $   $��	Edit Mode��5   $      	IsChecked$ FalseY�5   $      ToggleSwitchButtonStyle# [  5    $   5!   =   �� 5#      -   5        	 ��Name$	 	BtnUpdate��5#   "   $��0��6   $��Update��65   $	��100��5$      $��Center=�6   $��
10 15 0 15q�5%        SquareButtonStyle#���  5&      $y�Hidden=�5'      6%   �� 5)      -   5        $	 	BtnDelete��5)   "   $��0��6   $��Delete��65   $	��100��5*      $��Center=�6   $��
10 15 0 15q�5+      #���  5,      $y�Hidden=�5-      �� 50      -   5        $	 	BtnExport��50      $��0��6(   $��Export to Excel File��65   $��Center=�51      $��Right=�69   $��10 15 10 15q�6U   #���  52      6A   53      ]� 54   
   $��1��6   $
��Left=�6!   .+�55      �� -   5        
 ��Name$
 filter_options��55   $   $��1��6   $��
10 15 0 15q�6<   $	��106��6P    �� SelectedIndex$ 0��6\   56   {   �� 57      -   5         ��Name$ txt_filter_value��57   #   $��1��6   $��
10 15 0 15q�6=   $	��200��6Q   6]   �� 58      -   5        $
 office_locations��58   $   $��1��6   $��
10 15 0 15q�6>   $	��200��6R   $y�	Collapsed=�6^   59   N   5:      &   System.Windows.Controls.DataGrid  5;   
   -	   5          Name$ 
pcDataGrid��5;      $��2��5<      $��0,10,0,0q�5>         CanUserAddRows .     
IsReadOnly .    GridLinesVisibility$	 All=�5A         EnableRowVirtualization . !   EnableColumnVirtualization .    AutoGenerateColumns .     RowHeaderWidth$ 27Q�5E         HeadersVisibility$	 All=�5F         RowDetailsVisibilityMode$ VisibleWhenSelected=�5G         SelectionUnit$ CellOrRowHeader=�5I      ��5=      ��  5K        
errorStyle �� Triggers P� Property P� Value �� RelativeSource �� Path��%@  &	       �� 5L      " ��6+    5M      P� 5N       �@=�HasError . . �5O      ��  � ��
 �  ��6;   �� 5P       �@�ToolTip 5Q   !   ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5S      .�5U      ��  �@��TextWrapping$
 Wrap=�65   -
   5        5W      5X         Columns 5Z      4  .System.Windows.Controls.DataGridTemplateColumn  5[      ,  &System.Windows.Controls.DataGridColumn   HeaderStyle 5\      �� 5]      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5^      ��  � ��$ Center=�6K   5_         Header 5a      �� 5b      -   5          ��Name$  headerCheckBox��5b   #   6\   !   CellTemplate! 5d      �� 5e      .R�5f      �� $��Center=�6m   $��Center=�6�   $��Center=�6�   $��0��6�   " d� 	IsChecked" 6'   �� *
IsSelected+# �� UpdateSourceTrigger$# PropertyChanged=�6�   5g      5i      0  *System.Windows.Controls.DataGridTextColumn  5j      $   Width5  /System.Windows.Controls.DataGridLengthConverter$
$ Auto 6%   -   5        pC_NameColumn��$
 Tên��5j   J   1	  +System.Windows.Controls.DataGridBoundColumn% 	  EditingElementStyle#% [ 6�   & 	  Binding& 6W   �� *	PC_Name+' �� ValidatesOnExceptions' . ( �� ValidatesOnDataErrors( . $# PropertyChanged=�6�     5k      $
$ Auto 6%   -   5        nVColumn��$ NV��5k   E   #% [ 6�   & 6Q   �� *NV+' . ( . $# PropertyChanged=�6�     5l      $
$ Auto 6%   -   5        nVCodeColumn��$ NVCode��5l   I   #% [ 6�   & 6Y   �� *NVCode+' . ( . $# PropertyChanged=�6�     5m      $
$ Auto 6%   -   5        pBColumn��$ PB��5m   E   #% [ 6�   & 6Q   �� *PB+' . ( . $# PropertyChanged=�6�     5n      $
$ Auto 6%   -   5        iPColumn��$ IP��5n   E   #% [ 6�   & 6Q   �� *IP+' . ( . $# PropertyChanged=�6�     5o      $
$ Auto 6%   -   5        	mACColumn��$	 MAC��5o   F   #% [ 6�   & 6S   �� *MAC+' . ( . $# PropertyChanged=�6�     5p      $
$ Auto 6%   -   5        
mAC2Column��$ MAC 2��5p   G   #% [ 6�   & 6V   �� *MAC2+' . ( . $# PropertyChanged=�6�     5q      $
$ Auto 6%   -   5        office_LocatedColumn��$ Office Located��5q   Q   #% [ 6�   & 6i   �� *Office_Located+' . ( . $# PropertyChanged=�6�     5r      $
$ Auto 6%   -   5        	cPUColumn��$	 CPU��5r   F   #% [ 6�   & 6S   �� *CPU+' . ( . $# PropertyChanged=�6�     5s      $
$ Auto 6%   -   5        	hDDColumn��$	 HDD��5s   F   #% [ 6�   & 6S   �� *HDD+' . ( . $# PropertyChanged=�6�     5t      $
$ Auto 6%   -   5        oSColumn��$ OS��5t   E   #% [ 6�   & 6Q   �� *OS+' . ( . $# PropertyChanged=�6�     5u      $
$ Auto 6%   -   5        	rAMColumn��$	 RAM��5u   F   #% [ 6�   & 6S   �� *RAM+' . ( . $# PropertyChanged=�6�     5v      $
$ Auto 6%   -   5        
typeColumn��$
 Type��5v   G   #% [ 6�   & 6U   �� *Type+' . ( . $# PropertyChanged=�6�     5w      $
$ Auto 6%   -   5        serviceTagColumn��$ Service Tag��5w   M   #% [ 6�   & 6b   �� *
ServiceTag+' . ( . $# PropertyChanged=�) 	  ElementStyle) 5x      �� 5y      " ��6    .�5z      ��  � ��$
 Wrap=�6=   5{      5}        5~      $
$ Auto 6%   -   5        modelColumn��$ Model��5~   H   #% [ 6�   & 6W   �� *Model+' . ( . $# PropertyChanged=�6�     5      $
$ Auto 6%   -   5        mouseColumn��$ Mouse��5   H   #% [ 6�   & 6W   �� *Mouse+' . ( . $# PropertyChanged=�6�     5�      $
$ Auto 6%   -   5        keyboardColumn��$ Keyboard��5�   K   #% [ 6�   & 6]   �� *
KeyBoard+' . ( . $# PropertyChanged=�6�     5�      $
$ Auto 6%   -   5        notesColumn��$ Notes��5�   H   #% [ 6�   & 6W   �� *Notes+' . ( . $# PropertyChanged=�6�   *   RowValidationRules* 5�      
  PC.Utils.PcValidation
  5�      ,  &System.Windows.Controls.ValidationRule+   ValidationStep$+ UpdatedValue=�6%   !,   RowValidationErrorTemplate, 5�      �� 5�      .R�5�      � - � ToolTip- 6   ��  �� *FindAncestor+. �� AncestorType)  #System.Windows.Controls.DataGridRow".  $) #(Validation.Errors)[0].ErrorContent�.+�5�      A� $��0��6"   
���  ��$��20��5�      $��20��5�      $��Center=�6)   $��Center=�6F   �� 5�      $��!��6$   $��20(�6-   $
��Bold"�5�      
�������$��Center=�5�      $��Center=�6;   5�      5�      5�      5�      5�      