   M S B A M L   `   `   `  ����R  NOutsourcingStaffSupport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:OutsourcingStaffSupportOutsourcingStaffSupport  5      &     OutsourcingStaffSupport.Settings   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:OutsourcingStaffSupport     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � 	Resources  5
       �� 
TargetType   	BorderOut �� Property �� Value  BorderIn 
 txtBox  MaterialDesignTextBox �� BasedOn  comboboxStyle  MaterialDesignComboBox�� 5   
   %�  &	        &	 E    &	 6    7  &	 �    7  �� 5      " ��6   .�5      ��  � ��$ 0 0 20 0q�6+   6>   �� 5       �@��CornerRadius$ 5��61   6=   �� 5       � �� �#3A3A3A6/   �� 5       � �� �#44444460   �� 5       � ��$ 1q�64   �� 5       � }� . 61   �� 5       � ��$ 40��6+   5      �� 5      " ��6   .�5      ��  � ��
 ���� 6/   �� 5       �@��CornerRadius$ 5��61   �� 5       � �� �#2D2D2D60   �� 5       � ��$ 2 2 0 0q�64   �� 5       � ��$ -1q�6+   5      �� 5      " ��6   8   .�5       ��  � ��$ 0q�64   �� 5!       � ��$	 5 1q�6,   �� 5"       � ��$ 3 0 0 0q�6+   5#      �� 5$      " ��6*   8   .�5%      ��  � ��$ 35��6+   �� 5&       � ��$ 
10 10 10 0q�6,   �� 5'       � ��$ 5 2 10 3q�6+   5(      5)      .��5+      (  "MaterialDesignThemes.Wpf.ColorZone  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=�6      CornerRadius$ 0 0 5 5��6P   $
��10 5q�6g   $
��10 0q�6v      Mode$
 Dark=�6�   .��5,   
   �� .+�5-      �� $��20(�6   
��� ���$��	0 10 0 20q�6:   $2��,[CompanyDataOutput] ENVファイルの設定��6M   �� 5.      -   5         ��Name$ 
spSettings��5.   0   	 �� Orientation$	 Vertical=�6   $��Stretch=�6D   $��Stretch=�6b   .+�5/      �� $	 
Horizontal=�6   .+�50      �� $��0 0 10 0q�6    $��Center=�63   $��CSV出力経路��6N   �� 51      #��[  6   $��1��6A   $��0��6Q   ._�52      �� #��[ 6!   ._�53      �� $	 
Horizontal=�6)   .+�54   "   �� -   5        
 ��Name$
 txt_Path��54   �   #��[ 6*   $��Center=�6J   
������ $��0q�6~    �� 
IsReadOnly . 6�   �� 56   "   -   5        $��btnBrowseFolderName��56   :   $��5 1 1 1q�6)   .��6w   Browse6   57      58      59      5:      5;      5<      5>      5@      