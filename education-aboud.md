# Education Folder Structure

## Modules

1. Bu yerda **Ta'limga oid bo'lgan va mantiqiy ichki modullar** joylashadi.  
2. Har bir modul **o‘z domeni uchun javobgar** va boshqa modullar bilan **ichki darajada** bog‘lanadi.  
3. Modullar Education ichidagi barcha **biznes qoidalarni boshqaradi** va **mustaqil ishlashga qodir**.  

---

## External

1. Ushbu folder **Education’ning tashqi dunyo bilan bog‘lovchi qatlami** hisoblanadi.  
2. External modullar **ko‘prik (bridge/gateway)** vazifasini bajarib, Education domenidagi modullarni tashqi servislar bilan bog‘laydi.  
3. Bu yondashuvning asosiy maqsadi:  
   - **I.** Ichki modullarni tashqi integratsiya tafsilotlaridan ajratish.  
   - **II.** Bog‘lanishni soddalashtirish va mustaqillikni saqlash.  
   - **III.** Kengaytirish va o‘zgartirishni osonlashtirish (masalan, boshqa servisini ulash).  

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
