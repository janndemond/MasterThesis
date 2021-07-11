data_csv <- read.csv2('resultProcessMining.csv', na="na")
library(ggplot2)
library(tikzDevice)
library(tidyverse)
library(dplyr)

options(tz="DE")

### All work items ###
y <- data_csv$ProcessTimewithMax
z <- factor(data_csv$StartYear)
x <- reorder(data_csv$ProcessTypeName,y, median)

data <- data.frame(x = x, y = y, z = z )

tikz(file = "WorkITemType_boxplot.tex", width = 6, height = 4)
ggplot(data, aes(x= x, y=y)) + 
  stat_boxplot(geom = "errorbar", width=0.5)+
  geom_boxplot()+
  stat_summary(fun =mean, geom = "point", col="red")+
  stat_summary(fun =mean, geom = "text", col="red", vjust =1.5,aes (label =paste("", round(..y.., digits = 1))))+
  stat_summary(fun =median, geom = "text", col="black", vjust =1.5,aes (label =paste("", round(..y.., digits = 1))))+
  labs(x="Work Item Type", y="Throughput time in days", title = "2019-2021 Throughput Time grouped by Work Item Type")+
  scale_x_discrete(guide = guide_axis(angle = 90))  + 
  #coord_flip()+
  theme_bw() + 
  theme(plot.title = element_text(size = rel(1), vjust = 0, hjust = 0.5), 
        axis.title = element_text(size = rel(0.8)),
        axis.title.y = element_text( vjust=2 ),
        axis.title.x = element_text( vjust=-0.5 ))
dev.off()

data_csv$ProcessType <-factor(data_csv$ProcessType)
data_csv$StartYear <-factor(data_csv$StartYear)

tikz(file = "WorkITemType_bar.tex", width = 6, height = 4)
ggplot(data, aes(x=x , fill=z)) +
  geom_bar(color ="white")+
  labs(x="Work Item Type", y="Number of recorded Work Items", ,title = "2019-2021 Number of recorded Work Items grouped by Type and Year")+
  
  scale_x_discrete(guide = guide_axis(angle = 90))+
  #coord_flip()+
  scale_fill_brewer(palette="Blues")+
  theme_bw()+ 
  guides(fill=guide_legend(title="Start Year"))+
  #geom_text(aes(label = ..count..), stat = "count", position = "stack",vjust = 1.5) +
  theme(plot.title = element_text(size = rel(1), vjust = 0, hjust = 0.5), 
        axis.title = element_text(size = rel(0.8)),
        axis.title.y = element_text( vjust=2 ),
        axis.title.x = element_text( vjust=-0.5 ),
        legend.position = "bottom")
#+ theme(axis.text.y=element_blank())
dev.off()

#### WeekDays ###


y <- data_csv$ProcessTimewithMax
z <- factor(data_csv$StartYear)
x <- reorder(data_csv$WeekDay,y, median)

data <- data.frame(x = x, y = y, z = z )

tikz(file = "weekday_boxplot.tex", width = 6, height = 4)
ggplot(data, aes(x= x, y=y)) + 
  stat_boxplot(geom = "errorbar", width=0.5)+
  geom_boxplot()+
  stat_summary(fun =mean, geom = "point", col="red")+
  stat_summary(fun =mean, geom = "text", col="red", vjust =1.5,aes (label =paste("", round(..y.., digits = 1))))+
  stat_summary(fun =median, geom = "text", col="black", vjust =1.5,aes (label =paste("", round(..y.., digits = 1))))+
  labs(x="Work Item Type", y="Throughput time in days", title = "2019-2021 Throughput Time grouped by Weekday")+
  scale_x_discrete(guide = guide_axis(angle = 90))  + 
  #coord_flip()+
  theme_bw() + 
  theme(plot.title = element_text(size = rel(1), vjust = 0, hjust = 0.5), 
        axis.title = element_text(size = rel(0.8)),
        axis.title.y = element_text( vjust=2 ),
        axis.title.x = element_text( vjust=-0.5 ))
dev.off()

tikz(file = "WorkITemType_bar.tex", width = 6, height = 4)
ggplot(data, aes(x=x , fill=z)) +
  geom_bar(color ="white")+
  labs(x="Work Item Type", y="Number of recorded Work Items", ,title = "2019-2021 Number of recorded Work Items by Weekday and Year")+
  
  scale_x_discrete(guide = guide_axis(angle = 90))+
  #coord_flip()+
  scale_fill_brewer(palette="Blues")+
  theme_bw()+ 
  guides(fill=guide_legend(title="Start Year"))+
  geom_text(aes(label = ..count..), stat = "count", position = "stack",vjust = 1.5) +
  theme(plot.title = element_text(size = rel(1), vjust = 0, hjust = 0.5), 
        axis.title = element_text(size = rel(0.8)),
        axis.title.y = element_text( vjust=2 ),
        axis.title.x = element_text( vjust=-0.5 ),
        legend.position = "bottom")
#+ theme(axis.text.y=element_blank())
dev.off()


# 