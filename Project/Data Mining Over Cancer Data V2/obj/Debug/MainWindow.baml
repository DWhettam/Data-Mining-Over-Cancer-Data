   M S B A M L   `   `   `  ����W  SData Mining Over Cancer Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Data_Mining_Over_Cancer_Data_V2Data_Mining_Over_Cancer_Data_V2  5   	   0    *Data_Mining_Over_Cancer_Data_V2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:Data_Mining_Over_Cancer_Data_V2     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ j48label��5
      $	��j48��6"   $
��Left=�60   $��	43,29,0,0q�6K   $	��Top=�6^   $��73��6v   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ 	nngelabel��5      $
��NNge��6#   $
��Left=�62   $��	43,65,0,0q�6M   $	��Top=�6`   $��73��6x   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ distancelabel��5      $��Weighted Distance��6'   $
��Left=�6C   $��
43,101,0,0q�6^   $	��Top=�6r   $	��155��6�   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ 	j48Result��5      $
��None��6#   $
��Left=�62   $��
239,34,0,0q�6M   $	��Top=�6a   �� 5   
   -   5        $ DistanceResult��5      $
��None��6(   $
��Left=�67   $��239,106,0,0q�6R   $	��Top=�6g   �� 5   
   -   5        $ 
NNgeResult��5      $
��None��6$   $
��Left=�63   $��
239,70,0,0q�6N   $	��Top=�6b   �� 5   
   -   5         ��Name$ moreDetailButton��5      $��More Detail��6+   $
��Left=�6A   $��401,245,0,0q�6\   $	��Top=�6q   $��93��6�   6�   �� 5   
   -   5        $ generateButton��5      $��Generate Recommendations��6)   $
��Left=�6L   $��332,279,0,0q�6g   $	��Top=�6|   $	��162��6�    �� IsCancel . 6�   �� 5   
   -	   5        $ recommendationLabel��5      $��Recommended Diagnosis:��6-   $
��Left=�6N   $��
42,149,0,0q�6i   $	��Top=�6}   $	��232��6�   $��18(�6�   $
��Bold"�6�   �� 5   
   -
   5        $ RecommendedDiagnosis��5      $
��None��6.   $
��Left=�6=   $��294,154,0,0q�6X   $	��Top=�6m   $��94��6�   $��14(�6�   $
��Bold"�6�   �� 5   
   -   5        $ perfectMatchLabel��5      $��Perfect Match Found:��6+   $
��Left=�6J   $��
44,186,0,0q�6e   $	��Top=�6y   $	��179��6�   $��16(�6�   $
��Bold"�6�   $y�Hidden=�6�   �� 5   
   -   5        $ perfectMatch��5      $
��None��6&   $
��Left=�65   $��239,188,0,0q�6P   $	��Top=�6e   $��14(�6}   $
��Bold"�6�   $y�Hidden=�6�   � 5   
   $��39,252,309,0q�6   $	��Top=�6%    � ColumnDefinitions 5      �� 5      $	��62* �6#   �� 5      $
��107* �6#   .+�5      �� -   5         ��Name$ txtNum��5      $��-22,27,36,-16q�6@    �� 
IsReadOnly . 6W   �� 5      -   5        $ cmdUp��5      $��26,27,3,-16q�6>   $��˄��6S   6_   �� 5      -   5        $ cmdDown��5      $��58,27,79,-16q�6@   $��˅��6W   $��2��6y   5      �� 5    
   -   5        $ givenDiagnosisLabel��5       $��Given Diagnosis:��6-   $
��Left=�6H   $��
44,220,0,0q�6c   $	��Top=�6w   $	��179��6�   $��16(�6�   $
��Bold"�6�   $y�Hidden=�6�   �� 5!   
   -   5        $ givenDiagnosis��5!      $
��None��6(   $
��Left=�67   $��239,222,0,0q�6R   $	��Top=�6g   $��14(�6   $
��Bold"�6�   $y�Hidden=�6�   5"      5#      