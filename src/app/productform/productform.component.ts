import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-productform',
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css']
})
export class ProductformComponent implements OnInit {


  productname: string
  form: FormGroup
  constructor(private formBuilder: FormBuilder) {

    this.form = this.formBuilder.group({
      price: ['', [Validators.required]],
      productname: ['', [Validators.required]],
      date: ['', [Validators.required]]
    });


  }

  ngOnInit() {
  }

}
