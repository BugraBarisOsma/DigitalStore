# DigitalStore

Senaryo :<br/>
<br/>
-Customer<br/>
<br/>
1 ) Programi calistirdiginizda oncelikle Register'dan uye olmalisiniz. Uye olduktan sonra sizin otomatik olarak hem bir uyeliginiz hem de bir order'iniz olacaktir. <br/>
2 ) Uyeliginizi tamamladiktan sonra Auth uzerinden login isleminizi yapabilirsiniz.<br/>
3 ) Login isleminiz tamamlandiktan sonra size bir token ve userId gelecektir. userId ile orderinizin durumuna bakabilirsiniz. Bunun icin Orders altindaki Get islemini userIdniz ile birlikte kullanin. <br/>
4 ) Urunleri orderiniza (yani sepetinize) eklemek icin Category altinda kategorilere gore urunleri siralayabilirsiniz. Ya da Product altinda butun urunleri siralayabilirsiniz.<br/>
5 ) Bir urunu sepetinize eklemek icin OrderDetail altindaki add-product API'sini kullanmaniz gerekir. Burada urunun ID'si , sizin orderId'niz ve urun adediniz giriniz.<br/>
6 ) Satin alma islemi icin oncelikle kullanmak istediginiz kuponlari kontrol etmek icin Coupons altinda Get islemini kullaniniz.<br/>
7 ) Puanlarinizi kontrol etmek icin de Users altinda bunu kontrol edebilirsiniz.<br/>
8 ) Checkout isleminde ise oncelikle sizden OrderId , CouponCode (Couponun adi , kodu degil) ve kullanmak istediginiz puan tutarini giriniz, istemiyorsaniz bos birakiniz<br/>
9 ) Islem sonucunda size toplam tutar ve puan bilgisi donecektir . <br/>
<br/>
- Admin<br/>
<br/>
1 ) Programi calistirdiginizda size hazir olarak verilmis (database icerisinde) admin bilgileriniz bulunacaktir. Bu bilgiler ile authorization yapabilirsiniz.<br/>
2 ) Admin kullanicisi buradaki butun CRUD islemlerini yapabilir ve kontrol edebilir.<br/>
<br/>
Postman dokumantasyon :<br/>
 <br/>
https://documenter.getpostman.com/view/26754664/2sA3s3JC88<br/>
