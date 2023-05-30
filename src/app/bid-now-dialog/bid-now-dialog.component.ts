import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GlobalConstants } from '../global-constants';
import { ApiService } from '../services/api.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddDialogComponent } from '../add-dialog/add-dialog.component'

@Component({
  selector: 'app-bid-now-dialog',
  templateUrl: './bid-now-dialog.component.html',
  styleUrls: ['./bid-now-dialog.component.css']
})
export class BidNowDialogComponent {

  consumerForm!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public item:any,
    private dialogRef: MatDialogRef<BidNowDialogComponent>) { }

  ngOnInit() {
    let minBidPrice = this.item.productBasePrice; //100
    this.consumerForm = this.formBuilder.group({
      id:this.item.id,
      consumerName:['',[Validators.required, Validators.pattern(GlobalConstants.nameRegex)]],
      consumerMail:['',[Validators.required, Validators.pattern(GlobalConstants.emailRegex)]],
      consumerBidPrice:['',[Validators.required,Validators.min(minBidPrice)]]
    });
    console.log("consumer form before filling data: ",this.consumerForm.value);
  }

  confirmBid() {
    var consumerData = this.consumerForm.value;
    var data = {
      consumerName: consumerData.consumerName,
      consumerMailId: consumerData.consumerMail,
      farmerId: this.item.id,
      biddedPrice: consumerData.consumerBidPrice
    }
    console.log("Consumer's Portal: after confirm bid: consumer data",consumerData);
    console.log("Consumer's Portal: after confirm bid:",data);

    if (this.consumerForm.valid) {
      this.api.consumerBidNow(data).subscribe({
        next: (response: any) => {
          // console.log('subcribed');
          alert("Consumer's Portal: consumer bidded for the prodcut successfully");
          this.consumerForm.reset();
          this.dialogRef.close('bidded');
        },
        error: (error: any) => {
          alert("Consumer's Portal: error in bidding the product by the consumer");
        }
      })
    }
  }

}
