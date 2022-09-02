using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Items;

public class PalletMover : ManejoPallets {

    public MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows
    }

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!Possession() && Desde.Possession() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (Possession() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Possession() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
                if (!Possession() && Desde.Possession() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (Possession() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Possession() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;
            default:
                break;
        }
    }

    void PrimerPaso() {
        Desde.Give(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso() {
        Give(Hasta);
        segundoCompleto = false;
    }

    public override void Give(ManejoPallets receptor) {
        if (Possession()) {
            if (receptor.Receive(pallets[0])) {
                pallets.RemoveAt(0);
            }
        }
    }
    public override bool Receive(Pallet pallet) {
        if (!Possession()) {
            pallet.Portador = this.gameObject;
            base.Receive(pallet);
            return true;
        }
        else
            return false;
    }
}
