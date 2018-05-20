# Data-Mining-Over-Cancer-Data
The Data used for this project is of a clinical nature and cannot be made available. Consequently, the public version of this project is not functional as the dataset is required.

This project is an attempt to provide a usable tool for health clinicians when diagnosing bowel cancer.
Three key data mining algorithms are used, which are outlined below. Each algorithm is considered independently and collectively to provide a recommended diagnosis to a clinician. Recommendations fall into one of four categories, making this a classification problem. These are:
Cancer,
Infection,
Polyp,
Normal

### J48
J48 is an implementation of the C4.5 decision tree algorithm used for classification problems such as this. It is widely used in data mining applications, ranking \#1 in the 2008 'Top 10 Algorithms in Data Mining' published by Springer. The Weka authors described C4.5 as "a landmark decision tree program that is probably the machine learning workhorse most widely used in practice to date", making it solid choice for this project.

### NNge
NNge is a nearest-neighbor like algorithm that has been used widely in the clinical domain. For more information, see:  

Brent Martin (1995). Instance-Based learning: Nearest Neighbor With Generalization. Hamilton, New Zealand.  
Sylvain Roy (2002). Nearest Neighbor With Generalization. Christchurch, New Zealand.  

### Weighted Distance
Weighted distance is a measure of the similarity between inputted patients and those in the dataset. The Euclidean Distance between patients is calculated and ReliefF attribute weightings are applied. This offers a measure of similarity between patients that considers the relative importance of attributes. The minimum, maximum and mean distances have been considered and both a boxplot and probability density function have been visualised. The plots allow a clear visualisation of the range and average similarity between the input patient and those in the dataset. 

## Using the Decision Support Tool
Patient records are inputted by the user through a .csv file. The records can then be accessed within the tool for classification. 

![Classifying Records](https://github.com/DWhettam/Data-Mining-Over-Cancer-Data/blob/master/Images/ClassifyingTesting.PNG)

### More Detail Window
The tool also displays a selection of metrics regarding a record's classification. Weighted Euclidean distance values are shown, as well as classifier accuracy, and links to graphs and Weka outputs. 

![More Details](https://github.com/DWhettam/Data-Mining-Over-Cancer-Data/blob/master/Images/MoreDetailWindow.PNG)

### Graphs
Both a box plot and probabilty density function are used to visualise the distribution of weighted Euclidean distances from the inputted patient to each target label. These graphs can be used to gain insight into the significance of a classification, and provide an effective way of visualising issues of data quality - little seperation in the curves is suggestive of poor data.

![Box Plot](https://github.com/DWhettam/Data-Mining-Over-Cancer-Data/blob/master/Images/Patient1_BoxPlot.PNG)

![Prob Density](https://github.com/DWhettam/Data-Mining-Over-Cancer-Data/blob/master/Images/Patient1_DensityPlot.PNG)



## Requirements
[OxyPlot](https://github.com/oxyplot/oxyplot)


