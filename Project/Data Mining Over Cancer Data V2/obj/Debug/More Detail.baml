   M S B A M L   `   `   `  ����W  SData Mining Over Cancer Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Data_Mining_Over_Cancer_Data_V2Data_Mining_Over_Cancer_Data_V2  5   	   1    +Data_Mining_Over_Cancer_Data_V2.More_Detail   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   9local-clr-namespace:Data_Mining_Over_Cancer_Data_V2  5   	     � Title$  More_Detail��5	   	   $��313.377��6   $��720.323��6.   .��5
      � .+�5   
   �� -   5         ��Name$ CancerLabel��5      $��Distance to Cancer��6%   $
��Left=�6B   $��
107,45,0,0q�6]   $	��Top=�6q   $
��Bold"�6�   �� 5   
   -   5        $ PatientIDLabel��5      $��Patient ID: ��6(   $
��Left=�6?   $��10,8,0,0q�6Z   $	��Top=�6l   $
��Bold"�6�   �� 5   
   -   5        $ 	PatientID��5      $�� ��6#   $
��Left=�6.   $��84,8,0,0q�6I   $	��Top=�6[    J� RenderTransformOrigin$ -0.103,0.049&�6s   $
��Bold"�6�   �� 5   
   -   5        $ 
PolypLabel��5      $��Distance to Polyp��6$   $
��Left=�6@   $��
254,45,0,0q�6[   $	��Top=�6o   $
��Bold"�6�   �� 5   
   -   5        $ NormalLabel��5      $��Distance to Normal��6%   $
��Left=�6B   $��
552,45,0,0q�6]   $	��Top=�6q   $
��Bold"�6�   �� 5   
   -   5        $ InfectionLabel��5      $��Distance to Infection��6(   $
��Left=�6H   $��
395,45,0,0q�6c   $	��Top=�6w   $
��Bold"�6�   �� 5   
   -   5        $ MinLabel��5      $��Minimum:��6"   $
��Left=�65   $��	26,89,0,0q�6P   $	��Top=�6c   $
��Bold"�6{   �� 5   
   -   5        $ MaxLael��5      $��Maximum:��6!   $
��Left=�64   $��
26,130,0,0q�6O   $	��Top=�6c   $
��Bold"�6{   �� 5   
   -	   5        $ 	MeanLabel��5      $��Mean:��6#   $
��Left=�63   $��
26,173,0,0q�6N   $	��Top=�6b   $
��Bold"�6z   �� 5   
   -
   5        $ 	CancerMin��5      $��Label��6#   $
��Left=�63   $��
107,89,0,0q�6N   $	��Top=�6b   �� 5   
   -   5        $ 
NormalMean��5      $��Label��6$   $
��Left=�64   $��545,173,0,0q�6O   $	��Top=�6d   �� 5   
   -   5        $ InfectionMean��5      $��Label��6'   $
��Left=�67   $��400,173,0,0q�6R   $	��Top=�6g   �� 5   
   -   5        $ 	PolypMean��5      $��Label��6#   $
��Left=�63   $��254,173,0,0q�6N   $	��Top=�6c   $ 0.816,0.423&�6{   �� 5   
   -   5        $ 	NormalMax��5      $��Label��6#   $
��Left=�63   $��545,130,0,0q�6N   $	��Top=�6c   �� 5   
   -   5        $ InfectionMax��5      $��Label��6&   $
��Left=�66   $��400,130,0,0q�6Q   $	��Top=�6f   �� 5   
   -   5        $ PolypMax��5      $��Label��6"   $
��Left=�62   $��254,130,0,0q�6M   $	��Top=�6b   �� 5   
   -   5        $ 
CancerMean��5      $��Label��6$   $
��Left=�64   $��107,173,0,0q�6O   $	��Top=�6d   �� 5   
   -   5        $ 	CancerMax��5      $��Label��6#   $
��Left=�63   $��107,130,0,0q�6N   $	��Top=�6c   �� 5   
   -   5        $ InfectionMin��5      $��Label��6&   $
��Left=�66   $��
400,89,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ 	NormalMin��5      $��Label��6#   $
��Left=�63   $��
545,89,0,0q�6N   $	��Top=�6b   �� 5   
   -   5        $ PolypMin��5      $��Label��6"   $
��Left=�62   $��
254,89,0,0q�6M   $	��Top=�6a   �� 5    
   -   5         ��Name$ BoxPotButton��5       $��View Box Plot��6'   $
��Left=�6?   $��
26,221,0,0q�6Z   $	��Top=�6n   $	��100��6�   $��22��6�   6�   �� 5!   
   -   5        $ DensityPlotButton��5!      $��View Density Plot��6,   $
��Left=�6H   $��161,221,0,0q�6c   $	��Top=�6x   $	��100��6�   $��22��6�   6�   5#      5$      