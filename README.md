# USB CAN DotNet

This project is about C# implementation for USB-to-CAN interface from IXXAT.  

This package is developed using visual studio 2019 community edition. 

이 패키지는 IXXAT 의 USB-to-CAN 장비를 사용하기 위한 C# 코드를 포함합니다.

## Intallation

It is required to install VCI V4 driver which can be downloaded from [this page](https://www.ixxat.com/technical-support/resources/downloads-and-documentation?ordercode=1.01.0281.12001).  

When installing the driver, choose SDK VCI4(.NET) components.  

After intallation, all the manuals will be located in \Program Files\HMS\IXXAT VCI 4.0\Manual  

## 설치

장비 사용 및 프로젝트 빌드를 위해 VCI V4 드라이버를 [다음 페이지](https://www.ixxat.com/technical-support/resources/downloads-and-documentation?ordercode=1.01.0281.12001)에서 다운로드 하십시오.  

설치 도중 컴포넌트 선택 화면에서 SDK VCI4(.NET)항목을 선택합니다.  


만약 패키지가 없어서 빌드되지 않는 경우 NuGet Package Manager Console을 열고 다음을 입력하십시오.  

```
Install-Package Ixxat.Vci4.StrongName
```

