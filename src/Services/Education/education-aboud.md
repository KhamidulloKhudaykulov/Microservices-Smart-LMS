# Education Folder Structure

## Modules

1. Bu yerda **Ta'limga oid bo'lgan va mantiqiy ichki modullar** joylashadi.  
2. Har bir modul **oСz domeni uchun javobgar** va boshqa modullar bilan **ichki darajada** bogСlanadi.  
3. Modullar Education ichidagi barcha **biznes qoidalarni boshqaradi** va **mustaqil ishlashga qodir**.  

---

## External

1. Ushbu folder **EducationТning tashqi dunyo bilan bogСlovchi qatlami** hisoblanadi.  
2. External modullar **koСprik (bridge/gateway)** vazifasini bajarib, Education domenidagi modullarni tashqi servislar bilan bogСlaydi.  
3. Bu yondashuvning asosiy maqsadi:  
   - **I.** Ichki modullarni tashqi integratsiya tafsilotlaridan ajratish.  
   - **II.** BogСlanishni soddalashtirish va mustaqillikni saqlash.  
   - **III.** Kengaytirish va oСzgartirishni osonlashtirish (masalan, boshqa servisini ulash).  

---

## (RU) Education Folder Structure

### Modules
- «десь располагаютс€ **внутренние модули**, св€занные с образованием.  
-  аждый модуль **отвечает за свой домен** и взаимодействует с другими модул€ми **на внутреннем уровне**.  
- ћодули управл€ют всеми **бизнес-правилами** внутри Education и способны работать **автономно**.  

### External
- Ёта папка €вл€етс€ **слоем**, соедин€ющим Education с внешним миром.  
- ¬нешние модули выполн€ют роль **моста (bridge/gateway)**, св€зыва€ доменные модули Education с внешними сервисами.  
- ќсновные цели данного подхода:  
  - **I.** ќтделение внутренних модулей от деталей внешней интеграции.  
  - **II.** ”прощение взаимодействи€ и сохранение независимости.  
  - **III.** ќблегчение расширени€ и модификации (например, подключение нового сервиса).  

---

## (ENG) Education Folder Structure

### Modules
- This folder contains the **internal modules** related to education.  
- Each module is **responsible for its own domain** and interacts with other modules **internally**.  
- Modules manage all **business rules** within Education and are capable of working **independently**.  

### External
- This folder serves as the **layer** that connects Education to the external world.  
- External modules act as a **bridge/gateway**, linking Education domain modules with external services.  
- The main goals of this approach are:  
  - **I.** To separate internal modules from external integration details.  
  - **II.** To simplify connections and maintain independence.  
  - **III.** To make extension and modification easier (e.g., integrating with a new service).  
